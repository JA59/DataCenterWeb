using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DataCenterLive.Clients
{
    /// <summary>
    /// Everything about a planned experiment stage
    /// Currently just the stage type
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    [Serializable]
    public class PlannedExperimentStage
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public PlannedExperimentStage()
        {
            S88StageMetadata = new S88StageMetadata();
            Order = 1;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="plannedExperimentStageType"></param>
        /// <param name="order"></param>
        public PlannedExperimentStage(S88StageType plannedExperimentStageType, int order)
        {
            S88StageMetadata = new S88StageMetadata(PlannedExperimentStageTypeHelper.GetPlannedExperimentStageType(plannedExperimentStageType));
            Order = order;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="plannedExperimentStageType"></param>
        /// <param name="order"></param>
        /// <param name="s88StageUniqueID"></param>
        public PlannedExperimentStage(S88StageType plannedExperimentStageType, int order, Guid s88StageUniqueID)
        {
            S88StageMetadata = new S88StageMetadata(PlannedExperimentStageTypeHelper.GetPlannedExperimentStageType(plannedExperimentStageType), s88StageUniqueID);
            Order = order;
        }

        /// <summary>
        /// Gets the type of stage.
        /// </summary>
        public S88StageType PlannedExperimentStageType
        {
            get { return PlannedExperimentStageTypeHelper.GetPlannedExperimentStageType(S88StageMetadata.S88StageName); }
        }

        /// <summary>
        /// Gets the order (sequential order) for this stage type.
        /// </summary>
        [DataMember]
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the S88 stage metadata.
        /// </summary>
        [DataMember]
        public S88StageMetadata S88StageMetadata { get; set; }
    }
}
