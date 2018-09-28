/* tslint:disable */

import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'demo-1',
  templateUrl: './demo-1.component.html',
  styleUrls: ['./demo-1.component.css']
})

export class Demo1Component {
  @Input('name') myName: string;
  @Output() selectEvent = new EventEmitter();

  constructor() {
  }

  setName(val: string) {
    this.myName = val;
  }

  itemSelected() {
    this.selectEvent.emit(this.myName);
  }
}


