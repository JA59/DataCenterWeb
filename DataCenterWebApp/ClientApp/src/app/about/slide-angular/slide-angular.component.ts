import { Component } from "@angular/core";

@Component({
  selector: "slide-angular",
  templateUrl: "./slide-angular.component.html",
  styleUrls: ['../slide.component.css']
})

export class SlideAngularComponent {
  slideNumber: number = 1;
  myName: string = 'joe';

  constructor() {
  }

  showSlide(index: number) {
    this.slideNumber = index;
  }

}
