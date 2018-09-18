import { Component, Inject, Input, OnInit, ViewChild, Injectable, ElementRef, AfterViewInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';
import { ISystemOverview } from '../interfaces/isystemoverview';
import { SystemOverview } from '../interfaces/systemoverview';
import { IPlannedExperiment } from '../interfaces/iplannedexperiment';
import { PlannedExperiment } from '../interfaces/plannedexperiment';
import { IPlannedExperimentsPage } from '../interfaces/iplannedexperimentspage';
import { PlannedExperimentsPage } from '../interfaces/plannedexperimentspage';
import { SystemOverviewService } from '../services/systemoverview.service';
import { ExperimentFilterPipe } from '../services/experimentfilter.pipe';
import { StatusEnum } from '../interfaces/status.enum';


//
// PageService
//
// The page service maintains a cache of the latest set of planned experiments.
//
// It makes the experiments available in pages of 20.
//
// The page service used the SystemOverviewService to be notified when a new systemOverview object is availble.  It
// used that object to determine if the set of planned experients has changed.  If so, it fetches the latest list of
// experiments, and separates them into pages, and sets the default page back to 1.
//

const ItemsPerPage = 10;

@Injectable()
export class PageService {
  StatusEnum = StatusEnum;
  private pageSubject = new Subject<IPlannedExperimentsPage>();                         // IPlannedExperimentsPage subject for observation
  private lastPlannedExperimentsPage: PlannedExperimentsPage;                           // cache of the last planned experiemnts page
  private plannedExperiments: PlannedExperiment[];                                      // cache of all planned experiments, used to create pages
  private subscription: Subscription | null = null;                                     // subscription to the SystemOverviewService
  private highestSequenceID: number = 0;                                                // last known highest sequence id - used to detect changes to the set ofexperiments
  private lastStatus: StatusEnum = StatusEnum.StatusUninitialized;
  private activeFilter: string = "";                                                    // filter string
  
  public pageObservable = this.pageSubject.asObservable();                              // Observable IPlannedExperimentsPage stream

  //
  // Constructor
  //
  constructor(private http: HttpClient, private systemOverviewService: SystemOverviewService)
  {
    // Init variables to reflect unknown status
    this.lastStatus = StatusEnum.StatusUninitialized;
    this.lastPlannedExperimentsPage = new PlannedExperimentsPage();
    this.lastPlannedExperimentsPage.Status = this.lastStatus;

    // Subscribe to the systemOverviewService
    this.subscription = this.systemOverviewService.systemOverviewObservable.subscribe(data => {
      console.log('page.service new status available: ' + data.ICDataCenterStatus);
      // New status available, see if we need to fetch new planned experiments
      if (data.ICDataCenterStatus == "Offline") {
        if (this.lastPlannedExperimentsPage.Status != StatusEnum.StatusOffine) {
          // We just toggled to offline
          this.lastStatus = StatusEnum.StatusOffine;
          this.highestSequenceID = 0;
          this.requestPage(1);
        }
      }
      else if (this.lastPlannedExperimentsPage.Status == StatusEnum.StatusUninitialized || data.HighestSequenceID != this.highestSequenceID || this.highestSequenceID == 0) {
        // not offline, but it appears we have never updated or don't have the latest sequence number
        console.log('PageService requesting new pages');
        // start a fetch of the latest planned experiements
        this.doFetchPlannedExperiments();
        this.highestSequenceID = data.HighestSequenceID;
      }
    });
  }

  //
  // getCurrentPage
  // Returns the most recently obtained IPlannedExperimentsPage
  //
  public getCurrentPage(): IPlannedExperimentsPage {
    return this.lastPlannedExperimentsPage;
  }

  //
  // requestPage(page)
  // Request that we switch to the specified page number
  // Will notify all clients about the newly selected page number
  //
  public requestPage(page: number) {
    if (this.lastStatus == StatusEnum.StatusUninitialized || this.lastStatus == StatusEnum.StatusOffine) {
      var pageObject = new PlannedExperimentsPage();
      pageObject.Status = this.lastStatus;
      this.updatePage(page, pageObject);
      return;
    }

    if (this.plannedExperiments.length == 0) {
      this.lastStatus = StatusEnum.StatusNoData;
      var pageObject = new PlannedExperimentsPage();
      pageObject.Status = this.lastStatus;
      this.updatePage(page, pageObject);
      return;
    }


    // apply the filter
    var filteredPlannedExperiments = new ExperimentFilterPipe().transform(this.plannedExperiments, this.activeFilter);
    if (filteredPlannedExperiments.length == 0) {
      this.lastStatus = StatusEnum.StatusNoFilteredData;
      var pageObject = new PlannedExperimentsPage();
      pageObject.Status = this.lastStatus;
      pageObject.UnfilteredRowCount = this.plannedExperiments.length;
      this.updatePage(page, pageObject);
      return;
    }

    // build the PlannedExperimentPage object for the requested page
    var pageObject = new PlannedExperimentsPage();
    this.lastStatus = StatusEnum.StatusGood;
    pageObject.Status = this.lastStatus;
    pageObject.PageSize = ItemsPerPage;
    pageObject.CurrentPage = page;

    // we have at least one row (experiment)
    // calculate how many pages, rows, etc.
    pageObject.RowCount = filteredPlannedExperiments.length;
    pageObject.PageCount = Math.floor((pageObject.RowCount + ItemsPerPage - 1) / pageObject.PageSize);
    pageObject.FirstRowOnPage = (page - 1) * pageObject.PageSize;
    pageObject.LastRowOnPage = pageObject.FirstRowOnPage + pageObject.PageSize - 1;
    // Make sure we don't go past the last available row
    if (pageObject.LastRowOnPage > pageObject.RowCount) {
      pageObject.LastRowOnPage = pageObject.RowCount;
    }
    // Grab the slice of the array that we need for the requested page
    pageObject.Results = filteredPlannedExperiments.slice(pageObject.FirstRowOnPage, pageObject.LastRowOnPage+1);
    pageObject.UnfilteredRowCount = this.plannedExperiments.length;
    pageObject.Filter = this.activeFilter;

    // notify the subscribers
    this.updatePage(page, pageObject);
  }

  public setFilter(filter: string)
  {
    this.activeFilter = filter;
    if (this.lastPlannedExperimentsPage.UnfilteredRowCount > 0) {
      // we have items, so filter them starting on page 1
      console.log('Filter changed, requesting page 1, status is ' + this.lastStatus);
      this.requestPage(1);
    }

  }

  public getFilter(): string {
    return this.activeFilter;
  }


  //
  // updatePage(page, data)
  // Notify subscribers about a page change
  //
  private updatePage(page: number, data: IPlannedExperimentsPage) {
    this.lastPlannedExperimentsPage = data;
    // let our subscribers know
    console.log('PageService notifying users status = ' + this.lastPlannedExperimentsPage.Status);
    this.pageSubject.next(data);
  }

  //
  // doFetchPlannedExperiments(tick)
  // Fetch the latest planned experiements object from the controller.
  //
  private doFetchPlannedExperiments() {
    this.http.get<IPlannedExperiment[]>("api/PlannedExperiment/ByAge").subscribe(result => {
      //console.log("Got planned experiments");
      this.plannedExperiments = result;
      this.lastStatus = StatusEnum.StatusGood;
      // new set of experiments, so go back to page 1
      this.requestPage(1);
    }, error => {
      console.log("Failed to get planned experiments");
     this.plannedExperiments = undefined;
      this.highestSequenceID = 0;
      this.lastStatus = StatusEnum.StatusOffine;
      this.requestPage(1); // offline
      console.log("Failed to obtain planned experiments");
      console.error(error)
    });
  }
}


