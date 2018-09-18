import { IPlannedExperiment } from './iplannedexperiment';
import { StatusEnum } from './status.enum';

export interface IPlannedExperimentsPage {
  Status: StatusEnum;
  CurrentPage: number;
  PageCount: number;
  PageSize: number; 
  RowCount: number;
  FirstRowOnPage: number;
  LastRowOnPage: number;
  UnfilteredRowCount: number;
  Filter: string;
  Results: IPlannedExperiment[];
}
