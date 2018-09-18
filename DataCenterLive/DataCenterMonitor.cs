using DataCenterLive.Clients;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataCenterLive
{
    public class DataCenterMonitor
    {
        private CancellationTokenSource m_CancellationTokenSource;
        private Task m_UpdateLoopTask;
        private string m_DataCenterHostName;
        private DataCenterClient m_DataCenterClient;
        private int m_highestSequenceId;

        private static readonly TimeSpan UpdateInterval = TimeSpan.FromSeconds(3);

        public DataCenterMonitor(string dataCenterHostName, DataCenterClient dataCenterClient)
        {
            m_DataCenterHostName = dataCenterHostName;
            m_CancellationTokenSource = new CancellationTokenSource();
            m_DataCenterClient = dataCenterClient;
            m_highestSequenceId = -1;
            m_UpdateLoopTask = UpdateCacheLoop(m_CancellationTokenSource.Token);
        }

        public void StopMonitoring()
        {
            m_CancellationTokenSource.Cancel();
        }

        private async Task UpdateCacheLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var startTime = DateTime.Now;

                try
                {
                    var systemOverview = DataCenterCache.Instance.SystemOverview;
                    systemOverview.ICDataCenterAddress = m_DataCenterHostName;

                    try
                    {
                        // Use the sequence data to determine if the list of planned experiemnts has changed
                        var sequenceData = m_DataCenterClient.GetPlannedExperimentsSequenceData();
                        if (m_highestSequenceId != sequenceData.HighestSequenceID)
                        {
                            DataCenterCache.Instance.PlannedExperiments = m_DataCenterClient.GetAllPlannedExperiments();
                            systemOverview.ExperimentCount = sequenceData.ExperimentCount;
                            systemOverview.HighestSequenceID = sequenceData.HighestSequenceID;
                            systemOverview.LastImportDate = m_DataCenterClient.GetNewestExperimentDate();
                        }
                        systemOverview.ICDataCenterVersion = m_DataCenterClient.GetServerVersion().Substring(1); // skip the leading "v";
                        systemOverview.ICDataCenterStatus = m_DataCenterClient.GetServerStatus();

                        m_highestSequenceId = sequenceData.HighestSequenceID; // remember for next pass
                    }
                    catch (Exception)
                    {
                        systemOverview.LastImportDate = DateTime.MinValue;
                        systemOverview.ExperimentCount = -1; // unknown
                        systemOverview.HighestSequenceID = 0;
                        systemOverview.ICDataCenterVersion = String.Empty; // skip the leading "v"
                        systemOverview.ICDataCenterStatus = "Offline";

                        m_highestSequenceId = -1;
                    }

                    systemOverview.LastUpdate = DateTime.UtcNow;

                }
                catch (Exception exc)
                {
                    // log the error
                    Debug.WriteLine(string.Format("Exception updating data center cache {0}", exc));
                }

                // Total wait, including fetch times, is updateInterval
                // But, wait 1 second as a minimum
                var duration = DateTime.Now - startTime;
                var delay = UpdateInterval > duration ? UpdateInterval - duration : TimeSpan.FromSeconds(1);
                await Task.Delay(UpdateInterval, cancellationToken);
            }
        }
    }
}
