using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DataCenterLive.Clients
{

    /// <summary>
    /// Defines the operations available in the general purpose management service.
    /// </summary>
    [ServiceContract(Name = "IGeneralManagementService", Namespace = ServicesHelper.ServiceNameSpace)]
    public interface IGeneralManagementServiceAsync
    {
        /// <summary>
        /// Get the version string
        /// </summary>
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginGetServerVersion(System.AsyncCallback callback, object asyncState);

        ///<summary>
        /// Returns the result of calling GetServerVersion on the server that corresponds to the result.
        ///</summary>
        string EndGetServerVersion(System.IAsyncResult result);

        ///<summary>
        /// Returns the result of calling GetServerVersion on the server as an async Task.
        ///</summary>
        [OperationContract(AsyncPattern = true)]
        Task<string> GetServerVersionAsync();

        ///<summary>
        /// Calls GetServerVersion on the server and may or may not wait for a response see client.
        /// If this is synchronous it should not be called on the UI thread.
        ///</summary>
        string GetServerVersion();

        /// <summary>
        /// Get overall system state
        /// </summary>
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginGetSystemStateInfo(System.AsyncCallback callback, object asyncState);

        ///<summary>
        /// Returns the result of calling GetSystemStateInfo on the server that corresponds to the result.
        ///</summary>
        SystemStateInfo EndGetSystemStateInfo(System.IAsyncResult result);

        ///<summary>
        /// Returns the result of calling GetSystemStateInfo on the server as an async Task.
        ///</summary>
        [OperationContract(AsyncPattern = true)]
        Task<SystemStateInfo> GetSystemStateInfoAsync();

        ///<summary>
        /// Calls GetSystemStateInfo on the server and may or may not wait for a response see client.
        /// If this is synchronous it should not be called on the UI thread.
        ///</summary>
        SystemStateInfo GetSystemStateInfo();

    }

    /// <summary>
    /// Provides
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)] // This will be returned through a web service to the web page as well.
    [Serializable]
    public class SystemStateInfo
    {
        /// <summary>
        /// The error or warning summary
        /// </summary>
        [DataMember]
        public string ErrorWarningDescription { get; set; }
        /// <summary>
        /// The error or warning summary
        /// </summary>
        [DataMember]
        public SystemStatus CurrentSystemStatus { get; set; }
    }

    /// <summary>
    /// Enum for system status
    /// </summary>
    public enum SystemStatus
    {
        /// <summary>
        /// System has no errors or warnings
        /// </summary>
        OK,
        /// <summary>
        /// System has warning(s) but no errors
        /// </summary>
        Warnining,
        /// <summary>
        /// At least one error
        /// </summary>
        Error

    }
}
