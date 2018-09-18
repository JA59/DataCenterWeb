import { Component, Inject } from "@angular/core";
import { DraggableDirective } from '../services/draggable.directive';
import { DropTargetDirective } from '../services/droptarget.directive';
import { DragService } from '../services/drag.service';

@Component({
    selector: "drag-drop-test",
  templateUrl: "./drag-drop-test.component.html",
  styleUrls: ['./drag-drop-test.component.css']
})

export class DragDropTestComponent {

  myStylesZone1 = {
  }

  myStylesZone2 = {
  }

  onDropZone1(data: any) {
    var newColor = data;
    this.myStylesZone1 = {
      'background-color': newColor
    }
  }

  onDropZone2(data: any) {
    var newColor = data;
    this.myStylesZone2 = {
      'background-color': newColor
    }
  }
}
