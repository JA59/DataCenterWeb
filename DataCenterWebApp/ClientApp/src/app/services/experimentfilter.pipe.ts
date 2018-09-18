import { Pipe, PipeTransform } from '@angular/core';
import { IPlannedExperiment } from '../interfaces/iplannedexperiment';

@Pipe({
  name: 'experimentfilter'
})
export class ExperimentFilterPipe implements PipeTransform {
  transform(items: IPlannedExperiment[], searchText: string): any[] {
    if (!items) return [];
    if (!searchText) return items;
    searchText = searchText.toLowerCase();
    return items.filter(it => {
      return it.ExperimentName.toLowerCase().includes(searchText) ||
        it.User.toLowerCase().includes(searchText) ||
        it.Project.toLowerCase().includes(searchText) ||
        it.ProcessType.toLowerCase().includes(searchText);
    });
  }
}
