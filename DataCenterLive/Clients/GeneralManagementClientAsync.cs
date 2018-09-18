using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace DataCenterLive.Clients
{
    public class GeneralManagementClientAsync : ClientBase<IGeneralManagementServiceAsync>, IGeneralManagementServiceAsync
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GeneralManagementClientAsync(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }
        
        /// <summary>
         /// Get the server version
         /// </summary>
         /// <returns></returns>
        public IAsyncResult BeginGetServerVersion(System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetServerVersion(callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling GetServerVersion on the server that corresponds to the result.
        ///</summary>
        public string EndGetServerVersion(System.IAsyncResult result)
        {
            return base.Channel.EndGetServerVersion(result);
        }

        ///<summary>
        /// Returns the result of calling GetServerVersion on the server as an async Task.
        ///</summary>
        public Task<string> GetServerVersionAsync()
        {
            return base.Channel.GetServerVersionAsync();
        }

        ///<summary>
        /// Calls GetServerVersion on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public string GetServerVersion()
        {
            IAsyncResult result = BeginGetServerVersion(null, null);
            result.AsyncWaitHandle.WaitOne();

            return EndGetServerVersion(result);
        }

        /// <summary>
        /// Get overall system state
        /// </summary>
        public IAsyncResult BeginGetSystemStateInfo(System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetSystemStateInfo(callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling GetSystemStateInfo on the server that corresponds to the result.
        ///</summary>
        public SystemStateInfo EndGetSystemStateInfo(System.IAsyncResult result)
        {
            return base.Channel.EndGetSystemStateInfo(result);
        }

        ///<summary>
        /// Returns the result of calling GetSystemStateInfo on the server as an async Task.
        ///</summary>
        public Task<SystemStateInfo> GetSystemStateInfoAsync()
        {
            return base.Channel.GetSystemStateInfoAsync();
        }


        ///<summary>
        /// Calls GetSystemStateInfo on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public SystemStateInfo GetSystemStateInfo()
        {
            IAsyncResult result = BeginGetSystemStateInfo(null, null);
            result.AsyncWaitHandle.WaitOne();

            return EndGetSystemStateInfo(result);
        }
    }
}
