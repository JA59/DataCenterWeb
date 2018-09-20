import { Component } from '@angular/core';

@Component({
  selector: 'slide-dotnet',
  templateUrl: './slide-dotnet.component.html',
  styleUrls: ['../slide.component.css']
})

export class SlideDotNetComponent {
  slideNumber: number = 1;

  constructor() {
  }

  showSlide(index: number) {
    this.slideNumber = index;
  }

}
