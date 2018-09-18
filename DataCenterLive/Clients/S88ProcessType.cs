using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DataCenterLive.Clients
{

    /// <summary>
    /// Enumerated process type
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    [Serializable]
    public enum S88ProcessType
    {
        /// <summary>Unknown.</summary>
        [EnumMember]
        Unknown,

        /// <summary>ParticleTuningProcess.</summary>
        [EnumMember]
        ParticleTuningProcess,

        /// <summary>PurificationProcess.</summary>
        [EnumMember]
        PurificationProcess,

        /// <summary>ReprocessProcess.</summary>
        [EnumMember]
        ReprocessProcess,

        /// <summary>ReworkProcess.</summary>
        [EnumMember]
        ReworkProcess,

        /// <summary>Synthesis.</summary>
        [EnumMember]
        SynthesisProcess,
    }
}
