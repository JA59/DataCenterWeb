import { Component } from "@angular/core";

@Component({
  selector: "about",
  templateUrl: "./about.component.html",
  styleUrls: ['./about.component.css']
})

export class AboutComponent {
  title = "All About iC Data Center Web";
  slideNumber: number = 0;

  constructor() {
  }

  showSlide(index: number) {
    this.slideNumber = index;
  }

}
