import { ISystemOverview } from './isystemoverview';

export class SystemOverview implements ISystemOverview {
  LastImportDate: Date;

  ExperimentCount: number;

  HighestSequenceID: number;

  ICDataCenterAddress: string;

  ICDataCenterVersion: string;

  ICDataCenterStatus: string;

  DataCenterWebAppAddress: string;

  DataCenterWebAppVersion: string;

  DataCenterWebAppStatus: string;

  LoggedOnUser: string;

  LoggedOnRole: string;

  LastUpdate: Date;

}
