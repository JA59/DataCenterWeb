using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DataCenterLive.Clients
{

        /// <summary>
        /// Defines the operations available to planned experiments.
        /// </summary>
        [ServiceContract(Name = "IPlannedExperimentService", Namespace = ServicesHelper.ServiceNameSpace)]
        public interface IPlannedExperimentServiceAsync
        {
            /// <summary>
            /// Call that a client can make to upload an XML planned experiment
            /// </summary>
            [OperationContract(AsyncPattern = true)]
            IAsyncResult BeginUploadExperimentXml(string filename, byte[] data, System.AsyncCallback callback, object asyncState);

            ///<summary>
            /// Returns the result of calling UploadExperimentXml on the server that corresponds to the result.
            ///</summary>
            UploadExperimentXmlResult EndUploadExperimentXml(System.IAsyncResult result);

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling UploadExperimentXml on the server as an async Task.
            ///</summary>
            [OperationContract(AsyncPattern = true)]
            Task<UploadExperimentXmlResult> UploadExperimentXmlAsync(string filename, byte[] data);
#endif

            ///<summary>
            /// Calls UploadExperimentXml on the server and may or may not wait for a response see client.
            /// If this is synchronous it should not be called on the UI thread.
            ///</summary>
            UploadExperimentXmlResult UploadExperimentXml(string filename, byte[] data);

            /// <summary>
            /// Get all planned experiments
            /// </summary>
            [OperationContract(AsyncPattern = true)]
            IAsyncResult BeginGetAllPlannedExperiments(System.AsyncCallback callback, object asyncState);

            ///<summary>
            /// Returns the result of calling GetAllPlannedExperiments on the server that corresponds to the result.
            ///</summary>
            IEnumerable<PlannedExperiment> EndGetAllPlannedExperiments(System.IAsyncResult result);

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling GetAllPlannedExperiments on the server as an async Task.
            ///</summary>
            [OperationContract(AsyncPattern = true)]
            Task<IEnumerable<PlannedExperiment>> GetAllPlannedExperimentsAsync();
#endif

            ///<summary>
            /// Calls GetAllPlannedExperiments on the server and may or may not wait for a response see client.
            /// If this is synchronous it should not be called on the UI thread.
            ///</summary>
            IEnumerable<PlannedExperiment> GetAllPlannedExperiments();



        /// <summary>
        /// Get recent planned experiments
        /// </summary>
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginGetRecentPlannedExperiments(int max, System.AsyncCallback callback, object asyncState);

        ///<summary>
        /// Returns the result of calling GetRecentPlannedExperiments on the server that corresponds to the result.
        ///</summary>
        IEnumerable<PlannedExperiment> EndGetRecentPlannedExperiments(System.IAsyncResult result);

#if !SILVERLIGHT
        ///<summary>
        /// Returns the result of calling GetRecentPlannedExperiments on the server as an async Task.
        ///</summary>
        [OperationContract(AsyncPattern = true)]
        Task<IEnumerable<PlannedExperiment>> GetRecentPlannedExperimentsAsync(int max);
#endif

        ///<summary>
        /// Calls GetRecentPlannedExperiments on the server and may or may not wait for a response see client.
        /// If this is synchronous it should not be called on the UI thread.
        ///</summary>
        IEnumerable<PlannedExperiment> GetRecentPlannedExperiments(int max);






        /// <summary>
        /// Get the XML for a planned experiment in the requested version
        /// </summary>
        [OperationContract(AsyncPattern = true)]
            IAsyncResult BeginGetExperimentXmlAsVersion(Guid trackingId, string version, System.AsyncCallback callback, object asyncState);

            ///<summary>
            /// Returns the result of calling GetExperimentXmlAsVersion on the server that corresponds to the result.
            ///</summary>
            byte[] EndGetExperimentXmlAsVersion(System.IAsyncResult result);

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling GetExperimentXmlAsVersion on the server as an async Task.
            ///</summary>
            [OperationContract(AsyncPattern = true)]
            Task<byte[]> GetExperimentXmlAsVersionAsync(Guid trackingId, string version);
#endif

            ///<summary>
            /// Calls GetExperimentXmlAsVersion on the server and may or may not wait for a response see client.
            /// If this is synchronous it should not be called on the UI thread.
            ///</summary>
            byte[] GetExperimentXmlAsVersion(Guid trackingId, string version);

            /// <summary>
            /// Get the XML files that contain the S88 Process Definitions
            /// </summary>
            [OperationContract(AsyncPattern = true)]
            IAsyncResult BeginGetS88ProcessDefinitionFiles(System.AsyncCallback callback, object asyncState);

            ///<summary>
            /// Returns the result of calling GetS88ProcessDefinitionFiles on the server that corresponds to the result.
            ///</summary>
            FileVersionAndData[] EndGetS88ProcessDefinitionFiles(System.IAsyncResult result);

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling GetS88ProcessDefinitionFiles on the server as an async Task.
            ///</summary>
            [OperationContract(AsyncPattern = true)]
            Task<FileVersionAndData[]> GetS88ProcessDefinitionFilesAsync();
#endif

            ///<summary>
            /// Calls GetS88ProcessDefinitionFiles on the server and may or may not wait for a response see client.
            /// If this is synchronous it should not be called on the UI thread.
            ///</summary>
            FileVersionAndData[] GetS88ProcessDefinitionFiles();

            /// <summary>
            /// Get S88 Process Definition XML file for a specific version
            /// </summary>
            [OperationContract(AsyncPattern = true)]
            IAsyncResult BeginGetS88ProcessDefinitionFile(Version version, System.AsyncCallback callback, object asyncState);

            ///<summary>
            /// Returns the result of calling GetS88ProcessDefinitionFile on the server that corresponds to the result.
            ///</summary>
            string EndGetS88ProcessDefinitionFile(System.IAsyncResult result);

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling GetS88ProcessDefinitionFile on the server as an async Task.
            ///</summary>
            [OperationContract(AsyncPattern = true)]
            Task<string> GetS88ProcessDefinitionFileAsync(Version version);
#endif

            ///<summary>
            /// Calls GetS88ProcessDefinitionFile on the server and may or may not wait for a response see client.
            /// If this is synchronous it should not be called on the UI thread.
            ///</summary>
            string GetS88ProcessDefinitionFile(Version version);

            /// <summary>
            /// Get data about the range of sequence IDs
            /// </summary>
            [OperationContract(AsyncPattern = true)]
            IAsyncResult BeginGetPlannedExperimentsSequenceData(System.AsyncCallback callback, object asyncState);

            ///<summary>
            /// Returns the result of calling GetPlannedExperimentsSequenceData on the server that corresponds to the result.
            ///</summary>
            PlannedExperimentsSequenceData EndGetPlannedExperimentsSequenceData(System.IAsyncResult result);

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling GetPlannedExperimentsSequenceData on the server as an async Task.
            ///</summary>
            [OperationContract(AsyncPattern = true)]
            Task<PlannedExperimentsSequenceData> GetPlannedExperimentsSequenceDataAsync();
#endif

            ///<summary>
            /// Calls GetPlannedExperimentsSequenceData on the server and may or may not wait for a response see client.
            /// If this is synchronous it should not be called on the UI thread.
            ///</summary>
            PlannedExperimentsSequenceData GetPlannedExperimentsSequenceData();

            /// <summary>
            /// Get an array of SequenceItem within the specified range, ordered by sequence id
            /// An item is returned for every add or delete transaction within the range.
            /// </summary>
            [OperationContract(AsyncPattern = true)]
            IAsyncResult BeginGetOrderedTransactionSequenceItems(int startingSequenceId, int endingSequenceId, System.AsyncCallback callback, object asyncState);

            ///<summary>
            /// Returns the result of calling GetOrderedTransactionSequenceItems on the server that corresponds to the result.
            ///</summary>
            SequenceItem[] EndGetOrderedTransactionSequenceItems(System.IAsyncResult result);

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling GetOrderedTransactionSequenceItems on the server as an async Task.
            ///</summary>
            [OperationContract(AsyncPattern = true)]
            Task<SequenceItem[]> GetOrderedTransactionSequenceItemsAsync(int startingSequenceId, int endingSequenceId);
#endif

            ///<summary>
            /// Calls GetOrderedTransactionSequenceItems on the server and may or may not wait for a response see client.
            /// If this is synchronous it should not be called on the UI thread.
            ///</summary>
            SequenceItem[] GetOrderedTransactionSequenceItems(int startingSequenceId, int endingSequenceId);

            /// <summary>
            /// Get an array of SequenceItem ordered by squence id
            /// An item is returned for every existing planned experiment
            /// </summary>
            [OperationContract(AsyncPattern = true)]
            IAsyncResult BeginGetOrderedExistingSequenceItems(System.AsyncCallback callback, object asyncState);

            ///<summary>
            /// Returns the result of calling GetOrderedExistingSequenceItems on the server that corresponds to the result.
            ///</summary>
            SequenceItem[] EndGetOrderedExistingSequenceItems(System.IAsyncResult result);

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling GetOrderedExistingSequenceItems on the server as an async Task.
            ///</summary>
            [OperationContract(AsyncPattern = true)]
            Task<SequenceItem[]> GetOrderedExistingSequenceItemsAsync();
#endif

            ///<summary>
            /// Calls GetOrderedExistingSequenceItems on the server and may or may not wait for a response see client.
            /// If this is synchronous it should not be called on the UI thread.
            ///</summary>
            SequenceItem[] GetOrderedExistingSequenceItems();

            /// <summary>
            /// Gets the date/time of the newest planned experiment
            /// </summary>
            [OperationContract(AsyncPattern = true)]
            IAsyncResult BeginGetNewestExperimentDate(System.AsyncCallback callback, object asyncState);

            ///<summary>
            /// Returns the result of calling GetNewestExperimentDate on the server that corresponds to the result.
            ///</summary>
            DateTime EndGetNewestExperimentDate(System.IAsyncResult result);

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling GetNewestExperimentDate on the server as an async Task.
            ///</summary>
            [OperationContract(AsyncPattern = true)]
            Task<DateTime> GetNewestExperimentDateAsync();
#endif

            ///<summary>
            /// Calls GetNewestExperimentDate on the server and may or may not wait for a response see client.
            /// If this is synchronous it should not be called on the UI thread.
            ///</summary>
            DateTime GetNewestExperimentDate();
        }


}
