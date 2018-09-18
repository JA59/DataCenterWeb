using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DataCenterLive.Clients
{
    /// <summary>
    /// Defines the admin operations available to manage planned experiments.
    /// </summary>
    [ServiceContract(Name = "IPlannedExperimentServiceAdmin", Namespace = ServicesHelper.ServiceNameSpace)]
    public interface IPlannedExperimentServiceAdminAsync
    {
        /// <summary>
        /// Remove an experiment from the planned experiments
        /// </summary>
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginDeleteExperiment(Guid trackingId, System.AsyncCallback callback, object asyncState);

        ///<summary>
        /// Returns the result of calling DeleteExperiment on the server that corresponds to the result.
        ///</summary>
        void EndDeleteExperiment(System.IAsyncResult result);

#if !SILVERLIGHT
        ///<summary>
        /// Returns the result of calling DeleteExperiment on the server as an async Task.
        ///</summary>
        [OperationContract(AsyncPattern = true)]
        Task DeleteExperimentAsync(Guid trackingId);
#endif

        ///<summary>
        /// Calls DeleteExperiment on the server and may or may not wait for a response see client.
        /// If this is synchronous it should not be called on the UI thread.
        ///</summary>
        void DeleteExperiment(Guid trackingId);
    }
}
