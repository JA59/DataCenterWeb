import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'highlighter'
})

export class HighlighterPipe implements PipeTransform {
  transform(text: string, search): string {
    if (search && text) {
      let pattern = search.replace(/[-\[\]\/\{\}\(\)\*\+\?\.\\\^\$\|]/g, '//$&');
      pattern = pattern.split(' ').filter((t) => { return t.length > 0; }).join('|');
      const regex = new RegExp(pattern, 'gi');
      return text.replace(regex, (match) => `<span class="highlighter">${match}</span>`);
    }
    else {
      return text;
    }

  }
}


/** Usage:  
* <input type="text" [(ngModel)]="filter"> 
* <div [innerHTML]="some-string-member-in-component  | highlighter : filter"></div> 
*  
*/

//export class HighlighterPipe implements PipeTransform {
//  transform(text: string, search: string): string {
//    return search ? text.replace(new RegExp(search, 'i'), `<span class="highlighter">${search}</span>`) : text;
//  }
//}
