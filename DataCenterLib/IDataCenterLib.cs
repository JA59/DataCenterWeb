using System;
using System.Collections.Generic;

namespace DataCenterLib
{
    public interface IDataCenterLib
    {
        void DeleteExperiment(string trackingId);
        IEnumerable<PlannedExperimentViewModel> GetAllPlannedExperiments();
        PlannedExperimentViewModel GetPlannedExperiment(string trackingId);
        DateTime GetNewestExperimentDate();
        string GetPlannedExperimentAsSvg(string trackingId);
        PlannedExperimentsSequenceData GetPlannedExperimentsSequenceData();
        string GetServerStatus();
        string GetServerVersion();
        SystemOverview GetSystemOverview();
    }
}