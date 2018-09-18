using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DataCenterLib
{
    public class ClientAdmin
    {
        private string m_address;
        private string m_Url;
        public ClientAdmin(string address)
        {
            m_address = address;
            m_Url = String.Format(@"http://{0}/Service/PlannedExperimentManagementAdmin", m_address);
        }
        private IPlannedExperimentServiceAdminAsync m_plannedExperimentServiceAdminAsync;
        /// <summary>
        /// Gets an instance of the the IPlannedExperimentServiceAdminAsync connecting to the current CentralDataServerUrl
        /// </summary>
        public async Task<IPlannedExperimentServiceAdminAsync> GetPlannedExperimentServiceAdmin()
        {

            var client = await Task.Run(() => new PlannedExperimentClientAdminAsync(ServicesHelper.GetDefaultBinding(),
                    new EndpointAddress(m_Url)));

            return client;

        }

        public async void DeleteExperiment(Guid trackingId)
        {

            if (m_plannedExperimentServiceAdminAsync == null)
            {
                m_plannedExperimentServiceAdminAsync = await GetPlannedExperimentServiceAdmin();
            }

            await Task.Run(() => m_plannedExperimentServiceAdminAsync.DeleteExperimentAsync(trackingId));

            return;
        }
    }
}
