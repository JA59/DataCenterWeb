using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DataCenterLib
{

    /// <summary>
    /// Enumerated stage type
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    [Serializable]
    public enum S88StageType
    {
        /// <summary>Unknown.</summary>
        [EnumMember]
        Unknown,

        /// <summary>Crystallization.</summary>
        [EnumMember]
        CrystallizationStage,

        /// <summary>Decomposition.</summary>
        [EnumMember]
        DecompositionStage,

        /// <summary>Distillation Of Product.</summary>
        [EnumMember]
        DistillationOfProductStage,

        /// <summary>Drying.</summary>
        [EnumMember]
        DryingStage,

        /// <summary>Drying Of Solution.</summary>
        [EnumMember]
        DryingOfSolutionStage,

        /// <summary>Equipment Conditioning.</summary>
        [EnumMember]
        EquipmentConditioningStage,

        /// <summary>Equipment Preparation.</summary>
        [EnumMember]
        EquipmentPreparationStage,

        /// <summary>Extraction.</summary>
        [EnumMember]
        ExtractionStage,

        /// <summary>Filtration.</summary>
        [EnumMember]
        FiltrationStage,

        /// <summary>Homogenization.</summary>
        [EnumMember]
        HomogenizationStage,

        /// <summary>Isolation.</summary>
        [EnumMember]
        IsolationStage,

        /// <summary>Mixture Preparation.</summary>
        [EnumMember]
        MixturePreparationStage,

        /// <summary>Particle Size.</summary>
        [EnumMember]
        ParticleSizeStage,

        /// <summary>Reaction.</summary>
        [EnumMember]
        ReactionStage,

        /// <summary>SolventRemoval</summary>
        [EnumMember]
        SolventRemovalStage,

        /// <summary>SolventSwitch</summary>
        [EnumMember]
        SolventSwitchStage,

        /// <summary>Transfer.</summary>
        [EnumMember]
        TransferStage,

        /// <summary>Washing.</summary>
        [EnumMember]
        WashingStage,

        /// <summary>Waste Treatment.</summary>
        [EnumMember]
        WasteTreatmentStage
    }
}
