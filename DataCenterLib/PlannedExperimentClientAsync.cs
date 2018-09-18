using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace DataCenterLib
{

        /// <summary>
        /// Client to the PlannedExperiment service (non-admin)
        /// Functions that are available to all users (admin or non-admin)    
        /// </summary>
        public class PlannedExperimentClientAsync : ClientBase<IPlannedExperimentServiceAsync>, IPlannedExperimentServiceAsync
        {
            /// <summary>
            /// Constructor
            /// </summary>
            public PlannedExperimentClientAsync(Binding binding, EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
            {
            }

            /// <summary>
            /// Get all planned experiments
            /// </summary>
            public IAsyncResult BeginGetAllPlannedExperiments(System.AsyncCallback callback, object asyncState)
            {
                return base.Channel.BeginGetAllPlannedExperiments(callback, asyncState);
            }

            ///<summary>
            /// Returns the result of calling GetAllPlannedExperiments on the server that corresponds to the result.
            ///</summary>
            public IEnumerable<PlannedExperiment> EndGetAllPlannedExperiments(System.IAsyncResult result)
            {
                return base.Channel.EndGetAllPlannedExperiments(result);
            }

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling GetAllPlannedExperiments on the server as an async Task.
            ///</summary>
            public Task<IEnumerable<PlannedExperiment>> GetAllPlannedExperimentsAsync()
            {
                return base.Channel.GetAllPlannedExperimentsAsync();
            }
#else
        ///<summary>
        /// Returns the result of calling GetAllPlannedExperiments on the server as an async Task.
        ///</summary>
        public Task<IEnumerable<PlannedExperiment>> GetAllPlannedExperimentsAsync()
        {
            var taskSource = new TaskCompletionSource<IEnumerable<PlannedExperiment>>();
            BeginGetAllPlannedExperiments(asyncResult =>
            {
                try
                {
                    var result = EndGetAllPlannedExperiments(asyncResult);
                    taskSource.SetResult(result);
                }
                catch (Exception exception)
                {
                    taskSource.SetException(exception);
                }
            }, null);
            return taskSource.Task;
        }
#endif

            ///<summary>
            /// Calls GetAllPlannedExperiments on the server and waits for a response (synchronous).
            /// This should not be called on a UI thread
            ///</summary>
            public IEnumerable<PlannedExperiment> GetAllPlannedExperiments()
            {
                IAsyncResult result = BeginGetAllPlannedExperiments(null, null);
                result.AsyncWaitHandle.WaitOne();

                return EndGetAllPlannedExperiments(result);
            }


        /// <summary>
        /// Get all planned experiments
        /// </summary>
        public IAsyncResult BeginGetRecentPlannedExperiments(int max, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetRecentPlannedExperiments(max, callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling GetAllPlannedExperiments on the server that corresponds to the result.
        ///</summary>
        public IEnumerable<PlannedExperiment> EndGetRecentPlannedExperiments(System.IAsyncResult result)
        {
            return base.Channel.EndGetAllPlannedExperiments(result);
        }

#if !SILVERLIGHT
        ///<summary>
        /// Returns the result of calling GetAllPlannedExperiments on the server as an async Task.
        ///</summary>
        public Task<IEnumerable<PlannedExperiment>> GetRecentPlannedExperimentsAsync(int max)
        {
            return base.Channel.GetRecentPlannedExperimentsAsync(max);
        }
#else
        ///<summary>
        /// Returns the result of calling GetAllPlannedExperiments on the server as an async Task.
        ///</summary>
        public Task<IEnumerable<PlannedExperiment>> GetAllPlannedExperimentsAsync()
        {
            var taskSource = new TaskCompletionSource<IEnumerable<PlannedExperiment>>();
            BeginGetAllPlannedExperiments(asyncResult =>
            {
                try
                {
                    var result = EndGetAllPlannedExperiments(asyncResult);
                    taskSource.SetResult(result);
                }
                catch (Exception exception)
                {
                    taskSource.SetException(exception);
                }
            }, null);
            return taskSource.Task;
        }
#endif

        ///<summary>
        /// Calls GetAllPlannedExperiments on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public IEnumerable<PlannedExperiment> GetRecentPlannedExperiments(int max)
        {
            IAsyncResult result = BeginGetRecentPlannedExperiments(max, null, null);
            result.AsyncWaitHandle.WaitOne();

            return EndGetRecentPlannedExperiments(result);
        }







        /// <summary>
        /// Get the XML for a planned experiment
        /// </summary>
        public IAsyncResult BeginGetExperimentXmlAsVersion(Guid trackingId, string version, System.AsyncCallback callback, object asyncState)
            {
                return base.Channel.BeginGetExperimentXmlAsVersion(trackingId, version, callback, asyncState);
            }

            ///<summary>
            /// Returns the result of calling GetExperimentXmlAsVersion on the server that corresponds to the result.
            ///</summary>
            public byte[] EndGetExperimentXmlAsVersion(System.IAsyncResult result)
            {
                return base.Channel.EndGetExperimentXmlAsVersion(result);
            }

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling GetExperimentXmlAsVersion on the server as an async Task.
            ///</summary>
            public Task<byte[]> GetExperimentXmlAsVersionAsync(Guid trackingId, string version)
            {
                return base.Channel.GetExperimentXmlAsVersionAsync(trackingId, version);
            }
#else
        ///<summary>
        /// Returns the result of calling GetExperimentXmlAsVersion on the server as an async Task.
        ///</summary>
        public Task<byte[]> GetExperimentXmlAsVersionAsync(Guid trackingId, string version)
        {
            var taskSource = new TaskCompletionSource<byte[]>();
            BeginGetExperimentXmlAsVersion(trackingId, version, asyncResult =>
            {
                try
                {
                    var result = EndGetExperimentXmlAsVersion(asyncResult);
                    taskSource.SetResult(result);
                }
                catch (Exception exception)
                {
                    taskSource.SetException(exception);
                }
            }, null);
            return taskSource.Task;
        }
#endif

            ///<summary>
            /// Calls GetExperimentXmlAsVersion on the server and waits for a response (synchronous).
            /// This should not be called on a UI thread
            ///</summary>
            public byte[] GetExperimentXmlAsVersion(Guid trackingId, string version)
            {
                IAsyncResult result = BeginGetExperimentXmlAsVersion(trackingId, version, null, null);
                result.AsyncWaitHandle.WaitOne();

                return EndGetExperimentXmlAsVersion(result);
            }

            /// <summary>
            /// Upload an XML file that represents a new or modified planned experiment
            /// </summary>
            public IAsyncResult BeginUploadExperimentXml(string filename, byte[] data, System.AsyncCallback callback, object asyncState)
            {
                return base.Channel.BeginUploadExperimentXml(filename, data, callback, asyncState);
            }

            ///<summary>
            /// Returns the result of calling UploadExperimentXml on the server that corresponds to the result.
            ///</summary>
            public UploadExperimentXmlResult EndUploadExperimentXml(System.IAsyncResult result)
            {
                return base.Channel.EndUploadExperimentXml(result);
            }

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling UploadExperimentXml on the server as an async Task.
            ///</summary>
            public Task<UploadExperimentXmlResult> UploadExperimentXmlAsync(string filename, byte[] data)
            {
                return base.Channel.UploadExperimentXmlAsync(filename, data);
            }
#else
        ///<summary>
        /// Returns the result of calling UploadExperimentXml on the server as an async Task.
        ///</summary>
        public Task<UploadExperimentXmlResult> UploadExperimentXmlAsync(string filename, byte[] data)
        {
            var taskSource = new TaskCompletionSource<UploadExperimentXmlResult>();
            BeginUploadExperimentXml(filename, data, asyncResult =>
            {
                try
                {
                    var result = EndUploadExperimentXml(asyncResult);
                    taskSource.SetResult(result);
                }
                catch (Exception exception)
                {
                    taskSource.SetException(exception);
                }
            }, null);
            return taskSource.Task;
        }
#endif

            ///<summary>
            /// Calls UploadExperimentXml on the server and waits for a response (synchronous).
            /// This should not be called on a UI thread
            ///</summary>
            public UploadExperimentXmlResult UploadExperimentXml(string filename, byte[] data)
            {
                IAsyncResult result = BeginUploadExperimentXml(filename, data, null, null);
                result.AsyncWaitHandle.WaitOne();

                return EndUploadExperimentXml(result);
            }

            /// <summary>
            /// Get the XML files containing the S88 process definitions
            /// </summary>
            public IAsyncResult BeginGetS88ProcessDefinitionFiles(System.AsyncCallback callback, object asyncState)
            {
                return base.Channel.BeginGetS88ProcessDefinitionFiles(callback, asyncState);
            }

            ///<summary>
            /// Returns the result of calling GetS88ProcessDefinitionFiles on the server that corresponds to the result.
            ///</summary>
            public FileVersionAndData[] EndGetS88ProcessDefinitionFiles(System.IAsyncResult result)
            {
                return base.Channel.EndGetS88ProcessDefinitionFiles(result);
            }

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling GetS88ProcessDefinitionFiles on the server as an async Task.
            ///</summary>
            public Task<FileVersionAndData[]> GetS88ProcessDefinitionFilesAsync()
            {
                return base.Channel.GetS88ProcessDefinitionFilesAsync();
            }
#else
        ///<summary>
        /// Returns the result of calling GetS88ProcessDefinitionFiles on the server as an async Task.
        ///</summary>
        public Task<FileVersionAndData[]> GetS88ProcessDefinitionFilesAsync()
        {
            var taskSource = new TaskCompletionSource<FileVersionAndData[]>();
            BeginGetS88ProcessDefinitionFiles(asyncResult =>
            {
                try
                {
                    var result = EndGetS88ProcessDefinitionFiles(asyncResult);
                    taskSource.SetResult(result);
                }
                catch (Exception exception)
                {
                    taskSource.SetException(exception);
                }
            }, null);
            return taskSource.Task;
        }
#endif

            ///<summary>
            /// Calls GetS88ProcessDefinitionFiles on the server and waits for a response (synchronous).
            /// This should not be called on a UI thread
            ///</summary>
            public FileVersionAndData[] GetS88ProcessDefinitionFiles()
            {
                IAsyncResult result = BeginGetS88ProcessDefinitionFiles(null, null);
                result.AsyncWaitHandle.WaitOne();

                return EndGetS88ProcessDefinitionFiles(result);
            }

            /// <summary>
            /// Get the XML files containing the S88 process definitions
            /// </summary>
            public IAsyncResult BeginGetS88ProcessDefinitionFile(Version version, System.AsyncCallback callback, object asyncState)
            {
                return base.Channel.BeginGetS88ProcessDefinitionFile(version, callback, asyncState);
            }

            ///<summary>
            /// Returns the result of calling GetS88ProcessDefinitionFile on the server that corresponds to the result.
            ///</summary>
            public string EndGetS88ProcessDefinitionFile(System.IAsyncResult result)
            {
                return base.Channel.EndGetS88ProcessDefinitionFile(result);
            }

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling GetS88ProcessDefinitionFile on the server as an async Task.
            ///</summary>
            public Task<string> GetS88ProcessDefinitionFileAsync(Version version)
            {
                return base.Channel.GetS88ProcessDefinitionFileAsync(version);
            }
#else
        ///<summary>
        /// Returns the result of calling GetS88ProcessDefinitionFile on the server as an async Task.
        ///</summary>
        public Task<string> GetS88ProcessDefinitionFileAsync(Version version)
        {
            var taskSource = new TaskCompletionSource<string>();
            BeginGetS88ProcessDefinitionFile(version, asyncResult =>
            {
                try
                {
                    var result = EndGetS88ProcessDefinitionFile(asyncResult);
                    taskSource.SetResult(result);
                }
                catch (Exception exception)
                {
                    taskSource.SetException(exception);
                }
            }, null);
            return taskSource.Task;
        }
#endif

            ///<summary>
            /// Calls GetS88ProcessDefinitionFile on the server and waits for a response (synchronous).
            /// This should not be called on a UI thread
            ///</summary>
            public string GetS88ProcessDefinitionFile(Version version)
            {
                IAsyncResult result = BeginGetS88ProcessDefinitionFile(version, null, null);
                result.AsyncWaitHandle.WaitOne();

                return EndGetS88ProcessDefinitionFile(result);
            }

            /// <summary>
            /// Get data about the range of sequence IDs
            /// </summary>
            public IAsyncResult BeginGetPlannedExperimentsSequenceData(System.AsyncCallback callback, object asyncState)
            {
                return base.Channel.BeginGetPlannedExperimentsSequenceData(callback, asyncState);
            }

            ///<summary>
            /// Returns the result of calling GetPlannedExperimentsSequenceData on the server that corresponds to the result.
            ///</summary>
            public PlannedExperimentsSequenceData EndGetPlannedExperimentsSequenceData(System.IAsyncResult result)
            {
                return base.Channel.EndGetPlannedExperimentsSequenceData(result);
            }

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling GetPlannedExperimentsSequenceData on the server as an async Task.
            ///</summary>
            public Task<PlannedExperimentsSequenceData> GetPlannedExperimentsSequenceDataAsync()
            {
                return base.Channel.GetPlannedExperimentsSequenceDataAsync();
            }
#else
        ///<summary>
        /// Returns the result of calling GetPlannedExperimentsSequenceData on the server as an async Task.
        ///</summary>
        public Task<PlannedExperimentsSequenceData> GetPlannedExperimentsSequenceDataAsync()
        {
            var taskSource = new TaskCompletionSource<PlannedExperimentsSequenceData>();
            BeginGetPlannedExperimentsSequenceData(asyncResult =>
            {
                try
                {
                    var result = EndGetPlannedExperimentsSequenceData(asyncResult);
                    taskSource.SetResult(result);
                }
                catch (Exception exception)
                {
                    taskSource.SetException(exception);
                }
            }, null);
            return taskSource.Task;
        }
#endif

            ///<summary>
            /// Calls GetPlannedExperimentsSequenceData on the server and waits for a response (synchronous).
            /// This should not be called on a UI thread
            ///</summary>
            public PlannedExperimentsSequenceData GetPlannedExperimentsSequenceData()
            {
                IAsyncResult result = BeginGetPlannedExperimentsSequenceData(null, null);
                result.AsyncWaitHandle.WaitOne();

                return EndGetPlannedExperimentsSequenceData(result);
            }

            /// <summary>
            /// Get an array of SequenceItem within the specified range, ordered by sequence id
            /// An item is returned for every add or delete transaction within the range.
            /// </summary>
            public IAsyncResult BeginGetOrderedTransactionSequenceItems(int startingSequenceId, int endingSequenceId, System.AsyncCallback callback, object asyncState)
            {
                return base.Channel.BeginGetOrderedTransactionSequenceItems(startingSequenceId, endingSequenceId, callback, asyncState);
            }

            ///<summary>
            /// Returns the result of calling GetOrderedTransactionSequenceItems on the server that corresponds to the result.
            ///</summary>
            public SequenceItem[] EndGetOrderedTransactionSequenceItems(System.IAsyncResult result)
            {
                return base.Channel.EndGetOrderedTransactionSequenceItems(result);
            }

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling GetOrderedTransactionSequenceItems on the server as an async Task.
            ///</summary>
            public Task<SequenceItem[]> GetOrderedTransactionSequenceItemsAsync(int startingSequenceId, int endingSequenceId)
            {
                return base.Channel.GetOrderedTransactionSequenceItemsAsync(startingSequenceId, endingSequenceId);
            }
#else
        ///<summary>
        /// Returns the result of calling GetOrderedTransactionSequenceItems on the server as an async Task.
        ///</summary>
        public Task<SequenceItem[]> GetOrderedTransactionSequenceItemsAsync(int startingSequenceId, int endingSequenceId)
        {
            var taskSource = new TaskCompletionSource<SequenceItem[]>();
            BeginGetOrderedTransactionSequenceItems(startingSequenceId, endingSequenceId, asyncResult =>
            {
                try
                {
                    var result = EndGetOrderedTransactionSequenceItems(asyncResult);
                    taskSource.SetResult(result);
                }
                catch (Exception exception)
                {
                    taskSource.SetException(exception);
                }
            }, null);
            return taskSource.Task;
        }
#endif

            ///<summary>
            /// Calls GetOrderedTransactionSequenceItems on the server and waits for a response (synchronous).
            /// This should not be called on a UI thread
            ///</summary>
            public SequenceItem[] GetOrderedTransactionSequenceItems(int startingSequenceId, int endingSequenceId)
            {
                IAsyncResult result = BeginGetOrderedTransactionSequenceItems(startingSequenceId, endingSequenceId, null, null);
                result.AsyncWaitHandle.WaitOne();

                return EndGetOrderedTransactionSequenceItems(result);
            }

            /// <summary>
            /// Get an array of SequenceItem ordered by squence id
            /// An item is returned for every existing planned experiment
            /// </summary>
            public IAsyncResult BeginGetOrderedExistingSequenceItems(System.AsyncCallback callback, object asyncState)
            {
                return base.Channel.BeginGetOrderedExistingSequenceItems(callback, asyncState);
            }

            ///<summary>
            /// Returns the result of calling GetOrderedExistingSequenceItems on the server that corresponds to the result.
            ///</summary>
            public SequenceItem[] EndGetOrderedExistingSequenceItems(System.IAsyncResult result)
            {
                return base.Channel.EndGetOrderedExistingSequenceItems(result);
            }

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling GetOrderedExistingSequenceItems on the server as an async Task.
            ///</summary>
            public Task<SequenceItem[]> GetOrderedExistingSequenceItemsAsync()
            {
                return base.Channel.GetOrderedExistingSequenceItemsAsync();
            }
#else
        ///<summary>
        /// Returns the result of calling GetOrderedExistingSequenceItems on the server as an async Task.
        ///</summary>
        public Task<SequenceItem[]> GetOrderedExistingSequenceItemsAsync()
        {
            var taskSource = new TaskCompletionSource<SequenceItem[]>();
            BeginGetOrderedExistingSequenceItems(asyncResult =>
            {
                try
                {
                    var result = EndGetOrderedExistingSequenceItems(asyncResult);
                    taskSource.SetResult(result);
                }
                catch (Exception exception)
                {
                    taskSource.SetException(exception);
                }
            }, null);
            return taskSource.Task;
        }
#endif

            ///<summary>
            /// Calls GetOrderedExistingSequenceItems on the server and waits for a response (synchronous).
            /// This should not be called on a UI thread
            ///</summary>
            public SequenceItem[] GetOrderedExistingSequenceItems()
            {
                IAsyncResult result = BeginGetOrderedExistingSequenceItems(null, null);
                result.AsyncWaitHandle.WaitOne();

                return EndGetOrderedExistingSequenceItems(result);
            }

            /// <summary>
            /// Gets the date/time of the newest planned experiment
            /// </summary>
            public IAsyncResult BeginGetNewestExperimentDate(System.AsyncCallback callback, object asyncState)
            {
                return base.Channel.BeginGetNewestExperimentDate(callback, asyncState);
            }

            ///<summary>
            /// Returns the result of calling GetNewestExperimentDate on the server that corresponds to the result.
            ///</summary>
            public DateTime EndGetNewestExperimentDate(System.IAsyncResult result)
            {
                return base.Channel.EndGetNewestExperimentDate(result);
            }

#if !SILVERLIGHT
            ///<summary>
            /// Returns the result of calling GetNewestExperimentDate on the server as an async Task.
            ///</summary>
            public Task<DateTime> GetNewestExperimentDateAsync()
            {
                return base.Channel.GetNewestExperimentDateAsync();
            }
#else
        ///<summary>
        /// Returns the result of calling GetNewestExperimentDate on the server as an async Task.
        ///</summary>
        public Task<DateTime> GetNewestExperimentDateAsync()
        {
            var taskSource = new TaskCompletionSource<DateTime>();
            BeginGetNewestExperimentDate(asyncResult =>
            {
                try
                {
                    var result = EndGetNewestExperimentDate(asyncResult);
                    taskSource.SetResult(result);
                }
                catch (Exception exception)
                {
                    taskSource.SetException(exception);
                }
            }, null);
            return taskSource.Task;
        }
#endif

            ///<summary>
            /// Calls GetNewestExperimentDate on the server and waits for a response (synchronous).
            /// This should not be called on a UI thread
            ///</summary>
            public DateTime GetNewestExperimentDate()
            {
                IAsyncResult result = BeginGetNewestExperimentDate(null, null);
                result.AsyncWaitHandle.WaitOne();

                return EndGetNewestExperimentDate(result);
            }
        }

}
