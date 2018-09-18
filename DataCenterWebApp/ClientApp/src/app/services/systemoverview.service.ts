import { Component, Inject, Input, OnInit, ViewChild, Injectable, ElementRef, AfterViewInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';
import { ISystemOverview } from '../interfaces/isystemoverview';
import { SystemOverview } from '../interfaces/systemoverview';

//
// SystemOverViewService
//
// The system overview service periodically polls the SystemOverviewController for the latest
// SystemOverview object, and stores it in a local variable (lastSystemOverview),
//
// A component can use this service to obtain the most recently obtained SystemOverview.
// The method getLatest() return the most recent SystemOverview.
//
// A component can also subscribe to the systemOverviewSubject to be notified of changes to the most recent
// SystemOverview.
//

@Injectable()
export class SystemOverviewService {
  
  private timer;                                                                        // periodic timer
  private sub: Subscription;                                                            // internal timer subscription object
  private systemOverviewSubject = new Subject<ISystemOverview>();                       // ISystemOverview subject for observation
  private lastSystemOverview: ISystemOverview;                                          // cache of the last system overview
    
  public systemOverviewObservable = this.systemOverviewSubject.asObservable();          // Observable ISystemOverview stream

  //
  // Constructor
  //
  constructor(private http: HttpClient) {
    this.lastSystemOverview = new SystemOverview();
    this.lastSystemOverview.DataCenterWebAppStatus = 'Offline';
    this.lastSystemOverview.ExperimentCount = -1;
    this.lastSystemOverview.ICDataCenterStatus = 'Unknown';
    this.lastSystemOverview.LastImportDate = undefined;
    this.lastSystemOverview.LastUpdate = undefined;
    this.lastSystemOverview.HighestSequenceID = 0;
  }

  //
  // Start the periodic update
  //
  public start() {
    this.timer = Observable.timer(2000, 1000); // after two seconds, fire every second
    // subscribing to a observable returns a subscription object
    this.sub = this.timer.subscribe(t => this.doFetch(t));
  }

  //
  // Stop the periodic update
  //
  public stop() {
    // stop subscribing to the timer object
    // (which stops the timer since we arethe only subscriber)
    this.sub.unsubscribe();
  }

  //
  // getLatest()
  // Returns the most recently obtained SystemOverview
  //
  public getLatest(): SystemOverview {
    //console.log("SystemOverviewService getLatest: " + this.lastSystemOverview.LastUpdate);
    return this.lastSystemOverview;
  }
  
  //
  // dofetch(tick)
  // Fetch the latest SystemOverview object from the controller.
  //
  private doFetch(tick) {
    // Get the latest from the controller
    this.http.get<ISystemOverview>('api/SystemOverview/Summary').subscribe(result => {
      // success - create a SystemOverview object from the returned data
      let systemOverview = new SystemOverview();
      systemOverview.DataCenterWebAppAddress = result.DataCenterWebAppAddress;
      systemOverview.DataCenterWebAppStatus = result.DataCenterWebAppStatus;
      systemOverview.DataCenterWebAppVersion = result.DataCenterWebAppVersion;
      systemOverview.ExperimentCount = result.ExperimentCount;
      systemOverview.HighestSequenceID = result.HighestSequenceID;
      systemOverview.ICDataCenterAddress = result.ICDataCenterAddress;
      systemOverview.ICDataCenterStatus = result.ICDataCenterStatus;
      systemOverview.ICDataCenterVersion = result.ICDataCenterVersion;
      systemOverview.LastImportDate = result.LastImportDate;
      systemOverview.LastUpdate = result.LastUpdate;
      systemOverview.LoggedOnRole = result.LoggedOnRole;
      systemOverview.LoggedOnUser = result.LoggedOnUser;
      // and process it
      this.processData(systemOverview);
    }, error => {
      // error - create a SystemOverview object that indicates the controller is not available
      let systemOverview = new SystemOverview();
      systemOverview.DataCenterWebAppStatus = 'Offline';
      systemOverview.ExperimentCount = -1;
      systemOverview.ICDataCenterStatus = 'Unknown';
      systemOverview.LastImportDate = undefined;
      systemOverview.LastUpdate = undefined;
      // and process it
      this.processData(systemOverview);
    });
  }

  //
  // processData(data)
  // Updates the locally cached SystemOverview, as well as sending it to all subscribers.
  //
  private processData(data: ISystemOverview) {
    this.lastSystemOverview = data;
    this.systemOverviewSubject.next(data);
  }
}


