import { Component, Inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { IPlannedExperiment } from '../interfaces/iplannedexperiment';
import { IPlannedExperimentsPage } from '../interfaces/iplannedexperimentspage';

@Component({
    selector: "experiment-item",
  templateUrl: './experiment-item.component.html',
  styleUrls: ['./experiment-item.component.css']
})

export class ExperimentItemComponent {
    plannedexperiment: IPlannedExperiment;
    plannedExperimentSvgUrl: string | null = null;

    constructor(private activatedRoute: ActivatedRoute,
        private router: Router,
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string) {

        // create an empty object from the PlannedExperiment interface
        this.plannedexperiment = <IPlannedExperiment>{};

        var id = this.activatedRoute.snapshot.params["id"];
        //console.log(id);
        if (id) {
            var url = this.baseUrl + "api/plannedexperiment/" + id;
            this.plannedExperimentSvgUrl = this.baseUrl + "api/plannedexperiment/svg/" + id;

            this.http.get<IPlannedExperiment>(url).subscribe(result => {
                this.plannedexperiment = result;
            }, error => console.error(error));
        }
        else {
            //console.log("Invalid id: routing back to home...");
            this.router.navigate(["home"]);
        }
    }

    onDelete() {
        //console.log("Asking to delete Planned Experiment " + this.plannedexperiment.TrackingId);
        if (confirm("Do you realy want to delete this planned experiment?")) {
            var url = this.baseUrl + "api/plannedexperiment/" + this.plannedexperiment.TrackingId;
            this.http
                .delete(url)
                .subscribe(res => {
                    //console.log("Planned Experiment " + this.plannedexperiment.TrackingId + " has been deleted.");
                    this.router.navigate(['home']);
                },
                error => console.log("delete failed: " + error));
        }
    }
}
