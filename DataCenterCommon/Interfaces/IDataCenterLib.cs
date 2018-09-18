using DataCenterCommon.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenterCommon.Interfaces
{
    public interface IDataCenterLib
    {
        void DeleteExperiment(string trackingId);
        IEnumerable<ElnExperiment> GetAllPlannedExperiments();
        ElnExperiment GetPlannedExperiment(string trackingId);
        string GetPlannedExperimentAsSvg(string trackingId);
        SystemOverview GetSystemOverview();
    }
}
