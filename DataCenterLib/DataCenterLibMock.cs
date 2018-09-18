using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;

namespace DataCenterLib
{
    public class DataCenterLibMock : IDataCenterLib
    {
        private Dictionary<string, PlannedExperimentViewModel> m_experiments;
        private int m_sequenceId;
        private SystemOverview m_SystemOverview;

        public DataCenterLibMock(string address)
        {
            m_experiments = new Dictionary<string, PlannedExperimentViewModel>();
            for (int i=0; i< 100; i++)
            {
                var e = new PlannedExperimentViewModel();
                var now = DateTime.Now;
                e.CreatedTime = now - TimeSpan.FromMinutes(437*(i+1));
                e.ExperimentName = String.Format("{0} {1}", e.CreatedTime.DayOfWeek, i);
                e.ProcessType = GetProcessType(i);
                e.Project = String.Format("{0} {1}", e.CreatedTime.ToString("MMMM"), e.CreatedTime.ToString("yyyy"));
                e.SchemaVersion = "2.0";
                e.Svg = String.Empty;
                e.TrackingId = Guid.NewGuid().ToString();
                e.UniqueElnId = e.ExperimentName;
                e.User = GetUser(i);
                m_experiments.Add(e.TrackingId, e);
            }

            m_sequenceId = 101;

            m_SystemOverview = new SystemOverview()
            {
                DataCenterWebAppAddress = GetHostName(),
                DataCenterWebAppVersion = "1.0.0.0",
                DataCenterWebAppStatus = "OK"
            };
            m_SystemOverview.ICDataCenterAddress = address;
            m_SystemOverview.ICDataCenterVersion = GetServerVersion();
            m_SystemOverview.ICDataCenterStatus = GetServerStatus();
        }

        public SystemOverview GetSystemOverview()
        {
            var sequenceData = GetPlannedExperimentsSequenceData();
            m_SystemOverview.ExperimentCount = sequenceData.ExperimentCount;
            m_SystemOverview.HighestSequenceID = sequenceData.HighestSequenceID;
            m_SystemOverview.LastUpdate = DateTime.Now.ToLongTimeString();

            return m_SystemOverview;
        }

        private string GetProcessType(int index)
        {
            int i = index % 5;
            switch(i)
            {
                case 0:
                    return "Particle Tuning";
                case 1:
                    return "Purification";
                case 2:
                    return "Reprocess";
                case 3:
                    return "Rework";
                default:
                    return "Synthesis";
            }
        }

        private string GetUser(int index)
        {
            int i = index % 4;
            switch (i)
            {
                case 0:
                    return "paul.mccartney";
                case 1:
                    return "john.lennon";
                case 2:
                    return "george.harrison";
                default:
                    return "ringo.starr";
            }
        }
        public void DeleteExperiment(string trackingId)
        {
            if (this.m_experiments.ContainsKey(trackingId))
            {
                this.m_experiments.Remove(trackingId);
            }

        }

        public IEnumerable<PlannedExperimentViewModel> GetAllPlannedExperiments()
        {
            return m_experiments.Values;
        }

        public DateTime GetNewestExperimentDate()
        {
            return m_experiments.Values.First().CreatedTime;
        }

        public PlannedExperimentViewModel GetPlannedExperiment(string trackingId)
        {
            if (m_experiments.ContainsKey(trackingId))
                return m_experiments[trackingId];

            return null;
        }

        public string GetPlannedExperimentAsSvg(string trackingId)
        {
            return string.Empty;
        }

        public PlannedExperimentsSequenceData GetPlannedExperimentsSequenceData()
        {

            var plannedExperimentsSequenceData = new PlannedExperimentsSequenceData();
            plannedExperimentsSequenceData.ExperimentCount = m_experiments.Values.Count();
            plannedExperimentsSequenceData.LowestSequenceID = 100;
            plannedExperimentsSequenceData.HighestSequenceID = m_sequenceId;
            return plannedExperimentsSequenceData;
        }

        public string GetServerStatus()
        {
            return "OK";
        }

        public string GetServerVersion()
        {
            return "6.1.99";
        }

        private string GetHostName()
        {
            string machineName;
            try
            {
                machineName = Dns.GetHostName();
                IPHostEntry hostEntry = Dns.GetHostEntry(machineName);
                if (hostEntry != null)
                {
                    machineName = hostEntry.HostName;
                }
            }
            catch (Exception)
            {
                machineName = Environment.MachineName;
            }
            var firstPeriod = machineName.IndexOf('.');
            if (firstPeriod > 0)
                return machineName.Substring(0, firstPeriod - 1);

            return machineName;
        }
    }
}
