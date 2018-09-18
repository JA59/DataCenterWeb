using DataCenterCommon.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DataCenterLive
{
    /// <summary>
    /// DataCenterCache stores a cache of system overview information and planned experiments.
    /// The intent is to seve all clients from a single cache odf data, and to only update the cache
    /// when the set of planned experiemnts has changed.
    /// 
    /// The cache is manged by the DataCenterMonitor.
    /// </summary>
    public class DataCenterCache
    {
        private static DataCenterCache instance = null;
        private SystemOverview m_SystemOverview;
        private IEnumerable<ElnExperiment> m_plannedExperiments;

        public static DataCenterCache Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataCenterCache();
                return instance;
            }
        }

        public DataCenterCache()
        {
            m_SystemOverview = new SystemOverview()
            {
                DataCenterWebAppAddress = GetHostName(),
                DataCenterWebAppVersion = "1.0.0.0",
                DataCenterWebAppStatus = "OK"
            };
        }

        public SystemOverview SystemOverview
        {
            get
            {
                lock (instance)
                    return m_SystemOverview;
            }
            set
            {
                lock (instance)
                    m_SystemOverview = value;
            }
        }

        public IEnumerable<ElnExperiment> PlannedExperiments
        {
            get
            {
                lock (instance)
                    return m_plannedExperiments;
            }
            set
            {
                lock (instance)
                    m_plannedExperiments = value;
            }
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
