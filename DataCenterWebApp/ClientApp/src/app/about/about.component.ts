import { Component } from "@angular/core";

@Component({
  selector: "about",
  templateUrl: "./about.component.html",
  styleUrls: ['./about.component.css']
})

export class AboutComponent {
  title = "Implementing a web based solution with ASP.NET Core and Angular";
  slideNumber: number = 1;

  constructor() {
  }

  showSlide(index: number) {
    this.slideNumber = index;
  }

}
