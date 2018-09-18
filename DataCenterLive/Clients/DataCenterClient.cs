using DataCenterCommon.Svg;
using DataCenterCommon.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace DataCenterLive.Clients
{
    public class DataCenterClient
    {
        //private static Client m_Client;
        //private static ClientAdmin m_ClientAdmin;
        private string m_address;
        public DataCenterClient(string address)
        {
            m_address = address;
        }

        public IEnumerable<ElnExperiment> GetAllPlannedExperiments()
        {
            var plannedExperiments = new List<ElnExperiment>();
            using (var client = new Client(m_address))
            {
                var experiments = client.GetAllPlannedExperiments().Result.OrderByDescending(x => x.CreatedTime);
                foreach (var experiment in experiments)
                {
                    var pe = new ElnExperiment()
                    {
                        TrackingId = experiment.TrackingId.ToString(),
                        ExperimentName = experiment.ExperimentName,
                        Project = experiment.Project,
                        User = experiment.UserName,
                        SchemaVersion = experiment.SchemaVersion,
                        ProcessType = PlannedExperimentProcessTypeHelper.GetPlannedExperimentProcessType(experiment.ProcessType),
                        UniqueElnId = experiment.UniqueElnId,
                        CreatedTime = experiment.CreatedTime.ToLocalTime(),
                        Selected = false
                    };
                    pe.Svg = String.Format("<svg width=\"300px\" height=\"300px\" xmlns=\"http://www.w3.org/2000/svg\"><g><text x=\"10\" y=\"50\" font-size=\"30px\">{0}</text></g></svg>", experiment.ExperimentName);
                    plannedExperiments.Add(pe);
                }
                return plannedExperiments;
            }
        }

        #region Private methods
        /// <summary>
        /// The version of the server
        /// </summary>
        /// <returns></returns>
        public string GetServerVersion()
        {
            using (var client = new Client(m_address))
            {
                return client.GetServerVersion().Result;
            }

        }

        /// <summary>
        /// The the server status
        /// </summary>
        /// <returns></returns>
        public string GetServerStatus()
        {
            using (var client = new Client(m_address))
            {
                var retVal = client.GetSystemStateInfo().Result;
                return retVal.CurrentSystemStatus.ToString();
            }
        }

        /// <summary>
        /// Get the most recent experiemnt creation date
        /// </summary>
        /// <returns></returns>
        public DateTime GetNewestExperimentDate()
        {
            using (var client = new Client(m_address))
            {
                var retVal = client.GetNewestExperimentDate().Result;
                return retVal;
            }
        }

        /// <summary>
        /// Get the sequence data that reflects changes to the list of planned experiments
        /// </summary>
        /// <returns></returns>
        public PlannedExperimentsSequenceData GetPlannedExperimentsSequenceData()
        {
            using (var client = new Client(m_address))
            {
                var retVal = client.GetPlannedExperimentsSequenceData().Result;
                return retVal;
            }
        }

        public void DeleteExperiment(string trackingId)
        {
            using (var clientAdmin = new ClientAdmin(m_address))
            {
                clientAdmin.DeleteExperiment(new Guid(trackingId));
                Thread.Sleep(500);
            }
        }

        public string GetPlannedExperimentAsSvg(string trackingId)
        {
            using (var client = new Client(m_address))
            {
                var xmlBytes = client.GetExperimentXmlAsVersion(new Guid(trackingId)).Result;
                using (MemoryStream ms = new MemoryStream(xmlBytes))
                {
                    var xmlDocument = XDocument.Load(ms);
                    var generator = new SvgGenerator(xmlDocument);
                    return generator.GetSvg();
                }
            }
        }


        #endregion
    }
}
