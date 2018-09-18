import { IPlannedExperiment } from './iplannedexperiment';
import { PlannedExperiment } from './plannedexperiment';
import { IPlannedExperimentsPage } from './iplannedexperimentspage';
import { StatusEnum } from './status.enum';

export class PlannedExperimentsPage implements IPlannedExperimentsPage {
  Status: StatusEnum;
  CurrentPage: number;
  PageCount: number;
  PageSize: number;
  RowCount: number;
  FirstRowOnPage: number;
  LastRowOnPage: number;
  UnfilteredRowCount: number;
  Filter: string;
  Results: PlannedExperiment[];
}
