import { Component } from "@angular/core";

@Component({
  selector: "slide-introduction",
  templateUrl: "./slide-introduction.component.html",
  styleUrls: ['../slide.component.css']
})

export class SlideIntroductionComponent {
  slideNumber: number = 1;

  constructor() {
  }

  showSlide(index: number) {
    this.slideNumber = index;
  }

}
