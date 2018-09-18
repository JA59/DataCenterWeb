import { Component } from "@angular/core";

@Component({
  selector: "slide-overview",
  templateUrl: "./slide-overview.component.html",
  styleUrls: ['../slide.component.css']
})

export class SlideOverviewComponent {
  slideNumber: number = 1;

  constructor() {
  }

  showSlide(index: number) {
    this.slideNumber = index;
  }

}
