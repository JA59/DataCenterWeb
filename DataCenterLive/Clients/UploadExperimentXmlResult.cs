using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenterLive.Clients
{
    /// <summary>
    /// Result of an planned experiment upload
    /// </summary>
    [Serializable]
    public enum UploadExperimentXmlResult
    {
        /// <summary>
        /// General purpose (catch all)
        /// </summary>
        GeneralPurposeError = 0,

        /// <summary>
        /// Success
        /// </summary>
        Success = 1,

        /// <summary>
        /// Failed to validate against XSD schema
        /// </summary>
        FailedValidation = 2

    }
}
