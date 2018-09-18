using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace DataCenterLive.Clients
{
    /// <summary>
    /// Client to the PlannedExperiment service (admin)
    /// Functions that are only available to an administrator
    /// </summary>
    public class PlannedExperimentClientAdminAsync : ClientBase<IPlannedExperimentServiceAdminAsync>, IPlannedExperimentServiceAdminAsync
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PlannedExperimentClientAdminAsync(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        /// <summary>
        /// Delete an experiemnt by name
        /// </summary>
        public IAsyncResult BeginDeleteExperiment(Guid trackingId, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginDeleteExperiment(trackingId, callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling DeleteExperiment on the server that corresponds to the result.
        ///</summary>
        public void EndDeleteExperiment(System.IAsyncResult result)
        {
            base.Channel.EndDeleteExperiment(result);
        }

        ///<summary>
        /// Returns the result of calling DeleteExperiment on the server as an async Task.
        ///</summary>
        public Task DeleteExperimentAsync(Guid trackingId)
        {
            return base.Channel.DeleteExperimentAsync(trackingId);
        }

        ///<summary>
        /// Calls DeleteExperiment on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public void DeleteExperiment(Guid trackingId)
        {
            IAsyncResult result = BeginDeleteExperiment(trackingId, null, null);
            result.AsyncWaitHandle.WaitOne();

            EndDeleteExperiment(result);
        }
    }
}
