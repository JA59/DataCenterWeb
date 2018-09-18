using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenterLive.Clients
{


    /// <summary>
    /// Helper class for dealing with PlannedExperimentStageType enumeration
    /// </summary>
    public static class PlannedExperimentStageTypeHelper
    {
        /// <summary>
        /// Method to convert from a string to an enumerated value
        /// </summary>
        /// <param name="plannedExperimentStageType"></param>
        /// <returns></returns>
        public static S88StageType GetPlannedExperimentStageType(string plannedExperimentStageType)
        {
            switch (plannedExperimentStageType)
            {
                case "Crystallization":
                    return S88StageType.CrystallizationStage;
                case "Decomposition":
                    return S88StageType.DecompositionStage;
                case "DistillationOfProduct":
                    return S88StageType.DistillationOfProductStage;
                case "Drying":
                    return S88StageType.DryingStage;
                case "DryingOfSolution":
                    return S88StageType.DryingOfSolutionStage;
                case "EquipmentConditioning":
                    return S88StageType.EquipmentConditioningStage;
                case "EquipmentPreparation":
                    return S88StageType.EquipmentPreparationStage;
                case "Extraction":
                    return S88StageType.ExtractionStage;
                case "Filtration":
                    return S88StageType.FiltrationStage;
                case "Homogenization":
                    return S88StageType.HomogenizationStage;
                case "Isolation":
                    return S88StageType.IsolationStage;
                case "MixturePreparation":
                    return S88StageType.MixturePreparationStage;
                case "ParticleSize":
                    return S88StageType.ParticleSizeStage;
                case "Reaction":
                    return S88StageType.ReactionStage;
                case "SolventRemoval":
                    return S88StageType.SolventRemovalStage;
                case "SolventSwitch":
                    return S88StageType.SolventSwitchStage;
                case "Transfer":
                    return S88StageType.TransferStage;
                case "Washing":
                    return S88StageType.WashingStage;
                case "WasteTreatment":
                    return S88StageType.WashingStage;
                default:
                    return S88StageType.WasteTreatmentStage;
            }
        }

        /// <summary>
        /// Method to convert from a string to an enumerated value
        /// </summary>
        /// <param name="plannedExperimentStageType"></param>
        /// <returns></returns>
        public static string GetPlannedExperimentStageType(S88StageType plannedExperimentStageType)
        {
            switch (plannedExperimentStageType)
            {
                case S88StageType.CrystallizationStage:
                    return "Crystallization";
                case S88StageType.DecompositionStage:
                    return "Decomposition";
                case S88StageType.DistillationOfProductStage:
                    return "DistillationOfProduct";
                case S88StageType.DryingStage:
                    return "Drying";
                case S88StageType.DryingOfSolutionStage:
                    return "DryingOfSolution";
                case S88StageType.EquipmentConditioningStage:
                    return "EquipmentConditioning";
                case S88StageType.EquipmentPreparationStage:
                    return "EquipmentPreparation";
                case S88StageType.ExtractionStage:
                    return "Extraction";
                case S88StageType.FiltrationStage:
                    return "Filtration";
                case S88StageType.HomogenizationStage:
                    return "Homogenization";
                case S88StageType.IsolationStage:
                    return "Isolation";
                case S88StageType.MixturePreparationStage:
                    return "MixturePreparation";
                case S88StageType.ParticleSizeStage:
                    return "ParticleSize";
                case S88StageType.ReactionStage:
                    return "Reaction";
                case S88StageType.SolventRemovalStage:
                    return "SolventRemoval";
                case S88StageType.SolventSwitchStage:
                    return "SolventSwitch";
                case S88StageType.TransferStage:
                    return "Transfer";
                case S88StageType.WashingStage:
                    return "Washing";
                case S88StageType.WasteTreatmentStage:
                    return "WasteTreatment";
                default:
                    return "Unknown";
            }
        }
    }
}
