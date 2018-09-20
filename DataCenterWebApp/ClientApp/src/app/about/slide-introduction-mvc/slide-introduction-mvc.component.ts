import { Component } from "@angular/core";

@Component({
  selector: "slide-introduction-mvc",
  templateUrl: "./slide-introduction-mvc.component.html",
  styleUrls: ['../slide.component.css']
})

export class SlideIntroductionMvcComponent {
  slideNumber: number = 1;

  constructor() {
  }

  showSlide(index: number) {
    this.slideNumber = index;
  }

}
