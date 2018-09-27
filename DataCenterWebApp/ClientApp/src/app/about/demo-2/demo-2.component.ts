/* tslint:disable */
import { Component} from '@angular/core';

@Component({
  selector: 'demo-2',
  templateUrl: './demo-2.component.html',
  styleUrls: ['./demo-2.component.css']
})

export class Demo2Component {
  public names: string[];

  constructor() {
    this.names = new Array(4);
    this.names[0] ="SomeUser";
    this.names[1] = "SomeAdmin";
    this.names[2] = "Joe";
    this.names[3] = "Ed"; 
  }
}


