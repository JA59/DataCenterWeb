import { Component, Inject, Input, OnInit, OnDestroy, ViewEncapsulation } from "@angular/core";
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { Subscription } from 'rxjs/Subscription';
import { AuthService } from '../services/auth.service';
import { PageService } from '../services/page.service';
import { IPlannedExperiment } from '../interfaces/iplannedexperiment';
import { IPlannedExperimentsPage } from '../interfaces/iplannedexperimentspage';
import { HighlighterPipe } from "../services/highlighter.pipe";
import { StatusEnum } from '../interfaces/status.enum';

@Component({
    selector: "experiment-page",
  templateUrl: './experiment-page.component.html',
  styleUrls: ['./experiment-page.component.css'],
  encapsulation: ViewEncapsulation.None
})

export class ExperimentPageComponent implements OnInit, OnDestroy {
  StatusEnum = StatusEnum;
  @Input() class: string | undefined;
  title: string | undefined;
  selectedPlannedExperiment: IPlannedExperiment | undefined;
  plannedexperimentspage: IPlannedExperimentsPage | undefined;
  pageSet: number[] | undefined;
  subscription: Subscription | null = null;
  status: StatusEnum = StatusEnum.StatusUninitialized; 
  public searchText: string;
  justSelectedTrackingId: string | undefined;
  public selectedCount: number = 0;
  public maxSelectedCount: number = 0;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    public auth: AuthService,
    public pageService: PageService,
    private router: Router) {
  }

  ngOnInit() {
    console.log("PE ngOnInit");
    this.searchText = this.pageService.getFilter();
    this.status = StatusEnum.StatusUninitialized;
    this.plannedexperimentspage = this.pageService.getCurrentPage();
    this.showPage(this.plannedexperimentspage);
    this.subscription = this.pageService.pageObservable.subscribe(data => {
      this.showPage(data);
    });



  }

  //
  // ngOnDestroy()
  // Unsubscribe from the service
  //
  ngOnDestroy() {
    if (this.subscription != null) {
      this.subscription.unsubscribe();
    }
  }

  onSearchTextChange(newValue) {
    console.log(newValue);
    this.searchText = newValue; 
    // Apply the filter to the page service
    this.pageService.setFilter(this.searchText);
  }

  showPage(newPage: IPlannedExperimentsPage) {
    var firstPage: number;
    var lastPage: number;
    var i: number;

    console.log("PE told about status " + newPage.Status);
    this.plannedexperimentspage = newPage;

    // check authorization
    if (this.auth != undefined && this.auth.isLoggedInAsGuest()) {
      // not authorized
      this.status = StatusEnum.StatusUnauthorized;
      return;
    }

    // check bad status
    if (newPage.Status != StatusEnum.StatusGood) {
      this.status = newPage.Status;
      return;
    }

    // good to go
    //this.status = newPage.Status;
    //firstPage = this.plannedexperimentspage.CurrentPage;
    //lastPage = this.plannedexperimentspage.CurrentPage + 9;
    //if (this.plannedexperimentspage.PageCount < lastPage) lastPage = this.plannedexperimentspage.PageCount;
    //this.pageSet = new Array<number>(lastPage - firstPage + 1);
    //for (i = 0; i < lastPage - firstPage + 1; i++) {
    //    this.pageSet[i] = firstPage + i;
    //}

    this.selectedCount = 0;
    this.maxSelectedCount = newPage.LastRowOnPage - newPage.FirstRowOnPage + 1;

    this.status = newPage.Status;
    lastPage = this.plannedexperimentspage.PageCount;
    this.pageSet = new Array<number>(lastPage);
    for (i = 0; i < lastPage; i++) {
      this.pageSet[i] = i + 1;
    }
  }

  refresh() {

    //this.pageService.requestPage(this.plannedexperimentspage.CurrentPage);
    this.plannedexperimentspage = this.pageService.getCurrentPage();
    this.showPage(this.plannedexperimentspage);
  }

  onSelect(plannedexperiment: IPlannedExperiment) {
    // don't try to select the planned experiemnt that we just deleted
    if (this.justSelectedTrackingId == plannedexperiment.TrackingId) {
      this.justSelectedTrackingId = undefined; // reset it for next delete
      return;
    }
    this.selectedPlannedExperiment = plannedexperiment;
    this.router.navigate(["/plannedexperiment", this.selectedPlannedExperiment.TrackingId]);
  }

  getSelectedCount() {
    var i: number;
    var counter: number;
    counter = 0;
    for (i = 0; i < this.plannedexperimentspage.Results.length; i++) {
      if (this.plannedexperimentspage.Results[i].Selected) counter++;
    }
    this.selectedCount = counter;
    return counter;
  }

  setSelectionAll(state: boolean) {
    var i: number;
    for (i = 0; i < this.plannedexperimentspage.Results.length; i++) {
      this.plannedexperimentspage.Results[i].Selected = state;
    }
    this.getSelectedCount();
  }

  onSelected(plannedexperiment: IPlannedExperiment) {
    plannedexperiment.Selected = !plannedexperiment.Selected;
    this.justSelectedTrackingId = plannedexperiment.TrackingId;
    this.getSelectedCount();
    console.log("Max = " + this.maxSelectedCount + ", current = " + this.selectedCount);
  }

  onDelete() {
    var countToDelete: number;
    var i: number;
    var index: number;

    countToDelete = this.getSelectedCount();
    if (countToDelete > 0) {
      index = 0;
      var deleteSet = new Array<string>(countToDelete);
      var message = "Do you want to delete the following" + countToDelete + " experiments?";
      for (i = 0; i < this.plannedexperimentspage.Results.length; i++) {
        var plannedexperiment = this.plannedexperimentspage.Results[i];
        if (plannedexperiment.Selected) {
          deleteSet[index++] = plannedexperiment.TrackingId;
          message = message + "\n" + plannedexperiment.ExperimentName;
        }
      }
      
      if (confirm(message)) {
        for (i = 0; i < countToDelete; i++) {
          var url = this.baseUrl + "api/plannedexperiment/" + deleteSet[i];
          if (i == (countToDelete - 1)) {
            this.http
              .delete(url)
              .subscribe(res => {
                console.log("Planned Experiment " + deleteSet[i] + " has been deleted.");
                this.refresh(); // last item, so refresh
              },
                error => console.log("delete failed: " + error));
          } else {
            this.http
              .delete(url)
              .subscribe(res => {
                console.log("Planned Experiment " + deleteSet[i] + " has been deleted.");
              },
                error => console.log("delete failed: " + error));
          }

        }
      }
    }
  }


  requestPage(page: number) {
    console.log("PE requesting page " + page);

    this.pageService.requestPage(page);
  }

  goLogOn() {
    this.router.navigate(["/home"]);
  }
}
