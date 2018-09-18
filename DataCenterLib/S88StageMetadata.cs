using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DataCenterLib
{

    /// <summary>
    /// Class that contains metadata of the S88 stage.
    /// </summary>
    [Serializable]
    [DataContract]
    public class S88StageMetadata
    {
        /// <summary>
        ///default constructor
        /// </summary>
        public S88StageMetadata()
        {
            S88StageName = "Unknown";
            S88StageUniqueID = Guid.NewGuid();
        }
        /// <summary>
        /// S88 stage constructor with parameters
        /// </summary>
        /// <param name="s88stageName"></param>
        public S88StageMetadata(string s88stageName)
        {
            S88StageName = s88stageName;
            S88StageUniqueID = Guid.NewGuid();
        }
        /// <summary>
        /// S88 stage constructor with parameters
        /// </summary>
        /// <param name="s88stageName"></param>
        /// <param name="s88StageUniqueID"></param>
        public S88StageMetadata(string s88stageName, Guid s88StageUniqueID)
        {
            S88StageName = s88stageName;
            S88StageUniqueID = s88StageUniqueID;
        }
        /// <summary>
        /// Gets or sets the S88 stage unique identifier.
        /// </summary>
        [DataMember]
        public Guid S88StageUniqueID { get; set; }
        /// <summary>
        /// S88 stage Name(Kind)
        /// </summary>
        [DataMember]
        public string S88StageName { get; set; }
    }
}
