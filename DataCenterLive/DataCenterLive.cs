using DataCenterCommon.Interfaces;
using DataCenterCommon.Svg;
using DataCenterCommon.ViewModels;
using DataCenterLive.Clients;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace DataCenterLive
{
    public class DataCenterLive : IDataCenterLib
    {
        private DataCenterClient m_DataCenterClient;
        private DataCenterMonitor m_DataCenterMonitor;
        public DataCenterLive(string address)
        {
            m_DataCenterClient = new DataCenterClient(address);
            m_DataCenterMonitor = new DataCenterMonitor(address, m_DataCenterClient);
        }


        public void DeleteExperiment(string trackingId)
        {
            m_DataCenterClient.DeleteExperiment(trackingId);
        }

        public IEnumerable<ElnExperiment> GetAllPlannedExperiments()
        {
            return DataCenterCache.Instance.PlannedExperiments;//dc.GetAllPlannedExperiments();
        }

        public ElnExperiment GetPlannedExperiment(string trackingId)
        {
            return DataCenterCache.Instance.PlannedExperiments.Where(t => t.TrackingId == trackingId).FirstOrDefault();
        }

        public string GetPlannedExperimentAsSvg(string trackingId)
        {
            return m_DataCenterClient.GetPlannedExperimentAsSvg(trackingId);
        }

        public SystemOverview GetSystemOverview()
        {
            return DataCenterCache.Instance.SystemOverview;
        }
    }
}
