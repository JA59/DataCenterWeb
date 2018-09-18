using DataCenterCommon.Interfaces;
using DataCenterCommon.Svg;
using DataCenterCommon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DataCenterSimulated
{
    public class DataCenterSimulated : IDataCenterLib
    {
        private Dictionary<string, ElnExperiment> m_experiments;
        private int m_sequenceId;
        private SystemOverview m_SystemOverview;

        public DataCenterSimulated(string address)
        {
            m_experiments = new Dictionary<string, ElnExperiment>();
            for (int i = 0; i < 100; i++)
            {
                var e = new ElnExperiment();
                var now = DateTime.Now;
                e.CreatedTime = now - TimeSpan.FromMinutes(437 * (i + 1));
                e.ExperimentName = String.Format("{0} {1}", e.CreatedTime.DayOfWeek, i);
                e.ProcessType = GetProcessType(i);
                e.Project = String.Format("{0}", e.CreatedTime.ToString("MMMM yyyy"));
                e.SchemaVersion = "2.0";
                e.Svg = String.Empty;
                e.TrackingId = Guid.NewGuid().ToString();
                e.UniqueElnId = e.ExperimentName;
                e.User = GetUser(i);
                e.Selected = false;
                e.Svg = String.Format("<svg width=\"300px\" height=\"300px\" xmlns=\"http://www.w3.org/2000/svg\"><g><text x=\"10\" y=\"50\" font-size=\"30px\">{0}</text></g></svg>", e.ExperimentName);

                m_experiments.Add(e.TrackingId, e);
            }

            m_sequenceId = 101;

            m_SystemOverview = new SystemOverview()
            {
                DataCenterWebAppAddress = GetHostName(),
                DataCenterWebAppVersion = "1.0.0.0",
                DataCenterWebAppStatus = "OK"
            };
            m_SystemOverview.ICDataCenterAddress = "Internal";
            m_SystemOverview.ICDataCenterVersion = "6.1.99";
        }

        public SystemOverview GetSystemOverview()
        {
            // Update only those fields that may have changed
            m_SystemOverview.ExperimentCount = m_experiments.Values.Count();
            m_SystemOverview.HighestSequenceID = m_sequenceId;
            m_SystemOverview.LastUpdate = DateTime.UtcNow;
            m_SystemOverview.LastImportDate = m_experiments.Values.First().CreatedTime;
            m_SystemOverview.ICDataCenterStatus = "OK";
            return m_SystemOverview;
        }

        private string GetProcessType(int index)
        {
            int i = index % 5;
            switch (i)
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
                m_sequenceId++; // experiment list has changed
            }

        }

        public IEnumerable<ElnExperiment> GetAllPlannedExperiments()
        {
            return m_experiments.Values;
        }

        public ElnExperiment GetPlannedExperiment(string trackingId)
        {
            if (m_experiments.ContainsKey(trackingId))
                return m_experiments[trackingId];

            return null;
        }

        public string GetPlannedExperimentAsSvg(string trackingId)
        {
            if (m_experiments.ContainsKey(trackingId))
            {
                var exp = m_experiments[trackingId];
                var generator = new SvgGenerator(exp.ExperimentName, exp.User, exp.Project, exp.TrackingId);
                return generator.GetSvg();
            }
            return m_experiments[trackingId].Svg;

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
