using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DataCenterLib
{

        /// <summary>
        /// Class that represents a planned experiment, including the processes and stages
        /// </summary>
        [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
        [Serializable]
        public class PlannedExperiment
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// 
            public PlannedExperiment()
            {
                UniqueElnId = String.Empty;
                RequestedName = String.Empty;
                ExperimentName = String.Empty;
                UserName = String.Empty;
                Project = String.Empty;
                SchemaVersion = String.Empty;
                CreatedTime = DateTime.MinValue;
                TrackingId = Guid.Empty;
                ProcessType = S88ProcessType.Unknown;
                PlannedExperimentStages = new List<PlannedExperimentStage>();
                PlannedExperimentChemicals = new List<PlannedExperimentChemical>();
            }

            /// <summary>
            /// The external ID of the planned experiment
            /// </summary>
            [DataMember]
            public string UniqueElnId { get; set; }

            /// <summary>
            /// The name of the planned experiment requested in the XML file (optional)
            /// </summary>
            [DataMember]
            public string RequestedName { get; set; }

            /// <summary>
            /// The intended name for the planned experiment as used by iControl and the instrument
            /// </summary>
            [DataMember]
            public string ExperimentName { get; set; }

            /// <summary>
            /// Unique tracking identifier for the planned experiment
            /// </summary>
            [DataMember]
            public Guid TrackingId { get; set; }

            /// <summary>
            /// The name of the planned experiment
            /// </summary>
            [DataMember]
            public string UserName { get; set; }

            /// <summary>
            /// The project of the planned experiment
            /// </summary>
            [DataMember]
            public string Project { get; set; }

            /// <summary>
            /// The schema version of the planned experiment
            /// </summary>
            [DataMember]
            public string SchemaVersion { get; set; }

            /// <summary>
            /// The date/time the planned experiment was created
            /// </summary>
            [DataMember]
            public DateTime CreatedTime { get; set; }

            /// <summary>
            /// The process type
            /// </summary>
            [DataMember] public S88ProcessType ProcessType { get; set; }

            /// <summary>
            /// The list of stages for the planned experiment.
            /// </summary>
            [DataMember]
            public List<PlannedExperimentStage> PlannedExperimentStages { get; private set; }

            /// <summary>
            /// The list of chemicals for the planned experiment.
            /// </summary>
            [DataMember]
            public List<PlannedExperimentChemical> PlannedExperimentChemicals { get; private set; }
        }

}
