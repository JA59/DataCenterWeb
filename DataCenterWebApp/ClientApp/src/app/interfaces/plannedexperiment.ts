import { IPlannedExperiment } from './iplannedexperiment';

export class PlannedExperiment implements IPlannedExperiment  {
  TrackingId: string;
  ExperimentName: string;
  Project: string;
  User: string;
  CreatedTime: Date;
  SchemaVersion: string;
  ProcessType: string;
  UniqueElnId: string;
  Svg: string;
  Selected: boolean;
}
