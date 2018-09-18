using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace DataCenterLib
{
    public class DataCenterLib : IDataCenterLib
    {
        private static Client m_Client;
        private static ClientAdmin m_ClientAdmin;
        public DataCenterLib(string address)
        {
            if (m_Client == null)
            {
                m_Client = new Client(address);
            }
            if (m_ClientAdmin == null)
            {
                m_ClientAdmin = new ClientAdmin(address);
            }
        }

        /// <summary>
        /// Get a collection of planned experiemnts ordered by created time (most recent first)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PlannedExperimentViewModel> GetAllPlannedExperiments()
        {
            var plannedExperiments = new List<PlannedExperimentViewModel>();
            var experiments = m_Client.GetAllPlannedExperiments().Result.OrderByDescending(x => x.CreatedTime);
            foreach (var experiment in experiments)
            {
                var pe = new PlannedExperimentViewModel()
                {
                    TrackingId = experiment.TrackingId.ToString(),
                    ExperimentName = experiment.ExperimentName,
                    Project = experiment.Project,
                    User = experiment.UserName,
                    SchemaVersion = experiment.SchemaVersion,
                    ProcessType = PlannedExperimentProcessTypeHelper.GetPlannedExperimentProcessType(experiment.ProcessType),
                    UniqueElnId = experiment.UniqueElnId,
                    CreatedTime = experiment.CreatedTime.ToLocalTime(),
                };
                pe.Svg = String.Format("<svg width=\"300px\" height=\"300px\" xmlns=\"http://www.w3.org/2000/svg\"><g><text x=\"10\" y=\"50\" font-size=\"30px\">{0}</text></g></svg>", experiment.ExperimentName);
                plannedExperiments.Add(pe);
            }
            return plannedExperiments;
        }

        /// <summary>
        /// Delete a planned experiment
        /// </summary>
        /// <param name="trackingId">Id of the experiemnt to delete</param>
        public void DeleteExperiment(string trackingId)
        {
            m_ClientAdmin.DeleteExperiment(new Guid(trackingId));
            Thread.Sleep(500);
        }

        /// <summary>
        /// Get an SVG string that represents the planned experiment
        /// </summary>
        /// <param name="trackingId"></param>
        /// <returns></returns>
        public string GetPlannedExperimentAsSvg(string trackingId)
        {
            var xmlBytes = m_Client.GetExperimentXmlAsVersion(new Guid(trackingId)).Result;
            using (MemoryStream ms = new MemoryStream(xmlBytes))
            {
                var xmlDocument = XDocument.Load(ms);
                var generator = new SvgGenerator(xmlDocument);
                return generator.GetSvg();
            }
        }

        /// <summary>
        /// The version of the server
        /// </summary>
        /// <returns></returns>
        public string GetServerVersion()
        {
            return m_Client.GetServerVersion().Result;
        }

        /// <summary>
        /// The the server status
        /// </summary>
        /// <returns></returns>
        public string GetServerStatus()
        {
            var retVal = m_Client.GetSystemStateInfo().Result;
            return retVal.CurrentSystemStatus.ToString();
        }

        /// <summary>
        /// Get the most recent experiemnt creation date
        /// </summary>
        /// <returns></returns>
        public DateTime GetNewestExperimentDate()
        {
            var retVal = m_Client.GetNewestExperimentDate().Result;
            return retVal;
        }

        /// <summary>
        /// Get the sequence data that reflects changes to the list of planned experiments
        /// </summary>
        /// <returns></returns>
        public PlannedExperimentsSequenceData GetPlannedExperimentsSequenceData()
        {
            var retVal = m_Client.GetPlannedExperimentsSequenceData().Result;
            return retVal;
        }

        #region Obsolete stuff
        //public IEnumerable<PlannedExperimentViewModel> GetPlannedExperiments(int max)
        //{
        //    var plannedExperiments = new List<PlannedExperimentViewModel>();
        //    var experiments = m_Client.GetRecentPlannedExperiments(max).Result;
        //    foreach (var experiment in experiments)
        //    {
        //        var pe = new PlannedExperimentViewModel()
        //        {
        //            TrackingId = experiment.TrackingId.ToString(),
        //            ExperimentName = experiment.ExperimentName,
        //            Project = experiment.Project,
        //            User = experiment.UserName,
        //            SchemaVersion = experiment.SchemaVersion,
        //            ProcessType = PlannedExperimentProcessTypeHelper.GetPlannedExperimentProcessType(experiment.ProcessType),
        //            UniqueElnId = experiment.UniqueElnId,
        //            CreatedTime = experiment.CreatedTime.ToLocalTime(),
        //        };
        //        pe.Svg = String.Format("<svg width=\"300px\" height=\"300px\" xmlns=\"http://www.w3.org/2000/svg\"><g><text x=\"10\" y=\"50\" font-size=\"30px\">{0}</text></g></svg>", experiment.ExperimentName);
        //        plannedExperiments.Add(pe);
        //    }
        //    return plannedExperiments;
        //}

        public PlannedExperimentViewModel GetPlannedExperiment(string trackingId)
        {
            var plannedExperiments = GetAllPlannedExperiments();
            return plannedExperiments.Where(t => t.TrackingId == trackingId).FirstOrDefault();
        }

        public SystemOverview GetSystemOverview()
        {
            throw new NotImplementedException();
        }

        //private string GetXsl()
        //{
        //    Assembly myAssembly = Assembly.GetExecutingAssembly();
        //    using (Stream xslStream = myAssembly.GetManifestResourceStream("DataCenterLib.Resources.svg.xslt"))
        //    {
        //        using (TextReader tr = new StreamReader(xslStream))
        //        {
        //            return tr.ReadToEnd();
        //        }
        //    }
        //}

        //public static byte[] GetIcon()
        //{
        //    Assembly myAssembly = Assembly.GetExecutingAssembly();
        //    using (Stream xslStream = myAssembly.GetManifestResourceStream("DataCenterLib.Resources.task_filter_32.png"))
        //    {
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            xslStream.CopyTo(memoryStream);
        //            return memoryStream.ToArray();
        //        }
        //    }
        //}
        #endregion
    }
}
