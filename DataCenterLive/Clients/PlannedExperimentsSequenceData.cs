using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DataCenterLive.Clients
{
    /// <summary>
    /// Class that stores information about current sequence range 
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    [Serializable]
    public class PlannedExperimentsSequenceData
    {
        /// <summary>
        /// Lowest (oldest) sequence ID
        /// </summary>
        [DataMember]
        public int LowestSequenceID { get; set; }

        /// <summary>
        /// Highest (newest) sequence ID
        /// </summary>
        [DataMember]
        public int HighestSequenceID { get; set; }

        /// <summary>
        /// Number of experiments
        /// </summary>
        [DataMember]
        public int ExperimentCount { get; set; }
    }
}
