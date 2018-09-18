using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenterLib
{
    /// <summary>
    /// Helper class for dealing with PlannedExperimentProcessType enumeration
    /// </summary>
    public static class PlannedExperimentProcessTypeHelper
    {
        /// <summary>
        /// Method to convert from a string to an enumerated value
        /// </summary>
        /// <param name="plannedExperimentProcessType"></param>
        /// <returns></returns>
        public static S88ProcessType GetPlannedExperimentProcessType(string plannedExperimentProcessType)
        {
            switch (plannedExperimentProcessType)
            {
                case "ParticleTuning":
                    return S88ProcessType.ParticleTuningProcess;
                case "Purification":
                    return S88ProcessType.PurificationProcess;
                case "Reprocess":
                    return S88ProcessType.ReprocessProcess;
                case "Rework":
                    return S88ProcessType.ReworkProcess;
                case "Synthesis":
                    return S88ProcessType.SynthesisProcess;
                default:
                    return S88ProcessType.Unknown;
            }
        }

        /// <summary>
        /// Method to convert from an enumerated value to a string
        /// </summary>
        /// <param name="plannedExperimentProcessType"></param>
        /// <returns></returns>
        public static string GetPlannedExperimentProcessType(S88ProcessType plannedExperimentProcessType)
        {
            switch (plannedExperimentProcessType)
            {
                case S88ProcessType.ParticleTuningProcess:
                    return "ParticleTuning";
                case S88ProcessType.PurificationProcess:
                    return "Purification";
                case S88ProcessType.ReprocessProcess:
                    return "Reprocess";
                case S88ProcessType.ReworkProcess:
                    return "Rework";
                case S88ProcessType.SynthesisProcess:
                    return "Synthesis";
                default:
                    return "Unknown";
            }
        }
    }
}
