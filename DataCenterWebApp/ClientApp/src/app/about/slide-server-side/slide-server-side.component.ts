import { Component } from "@angular/core";

@Component({
  selector: "slide-server-side",
  templateUrl: "./slide-server-side.component.html",
  styleUrls: ['../slide.component.css']
})

export class SlideServerSideComponent {
  slideNumber: number = 1;

  constructor() {
  }

  showSlide(index: number) {
    this.slideNumber = index;
  }

}
