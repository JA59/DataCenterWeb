import { Component } from '@angular/core';

@Component({
  selector: 'slide-intro',
  templateUrl: './slide-intro.component.html',
  styleUrls: ['../slide.component.css']
})

export class SlideIntroComponent {
  slideNumber: number = 1;
  subSlideNumber: number = 1;

  constructor() {
  }

  showSlide(index: number) {
    this.slideNumber = index;
    this.subSlideNumber = 1;
  }

  showSubSlide(index: number) {
    this.subSlideNumber = index;
  }

}
