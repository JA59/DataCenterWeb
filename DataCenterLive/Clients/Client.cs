using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DataCenterLive.Clients
{
    public class Client : IDisposable
    {
        private string m_address;
        private string m_PlannedExperimentUrl;
        private string m_GeneralManagementUrl;
        public Client(string address)
        {
            m_address = address;
            m_PlannedExperimentUrl = String.Format(@"http://{0}/Service/PlannedExperimentManagement", m_address);
            m_GeneralManagementUrl = String.Format(@"http://{0}/Service/GeneralManagement", m_address);
        }

        private IPlannedExperimentServiceAsync m_plannedExperimentServiceAsync;
        private IGeneralManagementServiceAsync m_generalManagementServiceAsync;

        /// <summary>
        /// Gets an instance of the the IPlannedExperimentServiceAsync connecting to the current CentralDataServerUrl
        /// </summary>
        public async Task<IPlannedExperimentServiceAsync> GetPlannedExperimentService()
        {

            var client = await Task.Run(() => new PlannedExperimentClientAsync(ServicesHelper.GetDefaultBinding(),
                    new EndpointAddress(m_PlannedExperimentUrl)));

            return client;

        }

        /// Gets an instance of the the IPlannedExperimentServiceAsync connecting to the current CentralDataServerUrl
        /// </summary>
        public async Task<IGeneralManagementServiceAsync> GetGeneralManagementService()
        {

            var client = await Task.Run(() => new GeneralManagementClientAsync(ServicesHelper.GetDefaultBinding(),
                    new EndpointAddress(m_GeneralManagementUrl)));

            return client;

        }

        public async Task<IEnumerable<PlannedExperiment>> GetAllPlannedExperiments()
        {

            if (m_plannedExperimentServiceAsync == null)
            {
                m_plannedExperimentServiceAsync = await GetPlannedExperimentService();
            }

            var plannedExperiments = await Task.Run(() => m_plannedExperimentServiceAsync.GetAllPlannedExperimentsAsync());

            return plannedExperiments;
        }

        public async Task<IEnumerable<PlannedExperiment>> GetRecentPlannedExperiments(int max)
        {

            if (m_plannedExperimentServiceAsync == null)
            {
                m_plannedExperimentServiceAsync = await GetPlannedExperimentService();
            }

            var plannedExperiments = await Task.Run(() => m_plannedExperimentServiceAsync.GetRecentPlannedExperimentsAsync(max));

            return plannedExperiments;
        }

        public async Task<byte[]> GetExperimentXmlAsVersion(Guid trackingId)
        {

            if (m_plannedExperimentServiceAsync == null)
            {
                m_plannedExperimentServiceAsync = await GetPlannedExperimentService();
            }

            var experimentXml = await Task.Run(() => m_plannedExperimentServiceAsync.GetExperimentXmlAsVersionAsync(trackingId, "2.0"));

            return experimentXml;
        }

        public async Task<DateTime> GetNewestExperimentDate()
        {

            if (m_plannedExperimentServiceAsync == null)
            {
                m_plannedExperimentServiceAsync = await GetPlannedExperimentService();
            }

            var last = await Task.Run(() => m_plannedExperimentServiceAsync.GetNewestExperimentDate());

            return last;
        }

        public async Task<PlannedExperimentsSequenceData> GetPlannedExperimentsSequenceData()
        {

            if (m_plannedExperimentServiceAsync == null)
            {
                m_plannedExperimentServiceAsync = await GetPlannedExperimentService();
            }

            var sd = await Task.Run(() => m_plannedExperimentServiceAsync.GetPlannedExperimentsSequenceData());

            return sd;
        }

        public async Task<string> GetServerVersion()
        {

            if (m_generalManagementServiceAsync == null)
            {
                m_generalManagementServiceAsync = await GetGeneralManagementService();
            }

            var serverVersion = await Task.Run(() => m_generalManagementServiceAsync.GetServerVersion());

            return serverVersion;
        }

        public async Task<SystemStateInfo> GetSystemStateInfo()
        {

            if (m_generalManagementServiceAsync == null)
            {
                m_generalManagementServiceAsync = await GetGeneralManagementService();
            }

            var systemStateInfo = await Task.Run(() => m_generalManagementServiceAsync.GetSystemStateInfo());

            return systemStateInfo;
        }

        public void Dispose()
        {
            
        }
    }
}
