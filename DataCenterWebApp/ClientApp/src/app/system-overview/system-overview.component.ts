import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { SystemOverviewService } from '../services/systemoverview.service';
import { ISystemOverview } from '../interfaces/isystemoverview';

//
// system-overview component
//
// The system-overview component uses the SystemOverviewService to obtain the latest
// system overview information.  
// 
// It is also responsible for starting and stopping data collection by the SystemOverviewService.
//

@Component({
  selector: 'system-overview',
  templateUrl: './system-overview.component.html',
  styleUrls: ['./system-overview.component.css']
})

export class SystemOverviewComponent implements OnInit, OnDestroy {
  systemOverview: ISystemOverview;
  subscription: Subscription | null = null;

  //
  // Constructor
  // SystemOverviewService is injected
  //
  constructor(public systemOverviewService: SystemOverviewService) {
  }

  //
  // ngInit()
  // Get the latest SystemOverview object,
  // subscribe to new SystemOverview objects,
  // and then ask the service to start collecting.
  //
  ngOnInit() {
    this.systemOverview = this.systemOverviewService.getLatest();
    this.subscription = this.systemOverviewService.systemOverviewObservable.subscribe(data => {
      this.systemOverview = data;
    });

    this.systemOverviewService.start();
  }

  //
  // ngOnDestroy()
  // Ask the service to stop collecting, and
  // unsubscribe from the service
  //
  ngOnDestroy() {
    this.systemOverviewService.stop();
    if (this.subscription != null) {
      this.subscription.unsubscribe();
    }
  }
}
