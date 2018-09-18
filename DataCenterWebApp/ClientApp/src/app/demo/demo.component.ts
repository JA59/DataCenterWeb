import { Component, Inject } from "@angular/core";
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Router } from "@angular/router";
import { AuthService } from '../services/auth.service';
import { DemoUser } from '../interfaces/demouser';

@Component({
    selector: "demo",
  templateUrl: "./demo.component.html",
  styleUrls: ['./demo.component.css']
})

export class DemoComponent {
  title = "Demo";
  public items: DemoUser[];


    constructor(private router: Router,
        private authService: AuthService,
        @Inject('BASE_URL') private baseUrl: string) {
      this.items = new Array(12);
      this.items[0] = new DemoUser("SomeUser", "USER1");
      this.items[1] = new DemoUser("SomeAdmin", "ADMIN1");
      this.items[2] = new DemoUser("Joe", "JOE");
      this.items[3] = new DemoUser("Ed", "ED");

      for (var i = 4; i < 12; i++) {
        this.items[i] = new DemoUser("Disabled User"+i, "");;
      }
    }

    doLogin(username: string, password: string) {

        var url = this.baseUrl + "api/token/auth";

        this.authService.login(username, password)
            .subscribe(res => {
                // login successful - go show experiments
              this.router.navigate(["experiments"]);
            },
                err => {
                    // login failed

            });

    }
}
