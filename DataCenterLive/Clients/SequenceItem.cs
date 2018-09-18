using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenterLive.Clients
{
    /// <summary>
    /// Class that stores information about a sequence item 
    /// </summary>
    [Serializable]
    public class SequenceItem
    {
        /// <summary>
        /// Sequence number
        /// </summary>
        public int SequenceID { get; set; }

        /// <summary>
        /// Transaction type
        /// 1 = add, 2 = delete
        /// </summary>
        public int TransactionType { get; set; }

        /// <summary>
        /// The planned experiment
        /// May be null (even if type is add)
        /// </summary>
        public PlannedExperiment PlannedExperiment { get; set; }

        /// <summary>
        /// The Tracking ID for the experiment
        /// Used for delete
        /// </summary>
        public Guid? TrackingId { get; set; }
    }
}
