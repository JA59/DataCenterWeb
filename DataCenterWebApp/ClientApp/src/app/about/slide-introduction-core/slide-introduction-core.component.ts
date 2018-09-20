import { Component } from "@angular/core";

@Component({
  selector: "slide-introduction-core",
  templateUrl: "./slide-introduction-core.component.html",
  styleUrls: ['../slide.component.css']
})

export class SlideIntroductionCoreComponent {
  slideNumber: number = 1;

  constructor() {
  }

  showSlide(index: number) {
    this.slideNumber = index;
  }

}
