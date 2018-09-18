using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace DataCenterCommon.Svg
{
    public static class IconFactory
    {
        public static byte[] GetIcon(string operationType)
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            var resourceName = GetResourceName(operationType);
            if (String.IsNullOrEmpty(resourceName))
                return null;
            using (Stream xslStream = myAssembly.GetManifestResourceStream(resourceName))
            {
                using (var memoryStream = new MemoryStream())
                {
                    xslStream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        private static string GetResourceName(string operationType)
        {
            switch(operationType)
            {
                case "Centrifuge":
                    return "DataCenterCommon.Resources.task_centrifuge_32.png";
                case "Discharge":
                    return "DataCenterCommon.Resources.task_discharge_32.png";
                case "Dry":
                    return "DataCenterCommon.Resources.task_dry_32.png";
                case "Filter":
                    return "DataCenterCommon.Resources.task_filter_32.png";
                case "Homogenize":
                    return "DataCenterCommon.Resources.task_homigenize_32.png";
                case "Inert":
                    return "DataCenterCommon.Resources.task_inert_32.png";
                case "Mill":
                    return "DataCenterCommon.Resources.task_mill_32.png";
                case "PackAndLabel":
                    return "DataCenterCommon.Resources.task_pack_label_32.png";
                case "PressureAdjust":
                    return "DataCenterCommon.Resources.task_pressure_32.png";
                case "Recirculate":
                    return "DataCenterCommon.Resources.task_recirculate_32.png";
                case "Rinse":
                    return "DataCenterCommon.Resources.task_rinse_32.png";
                case "Separate":
                    return "DataCenterCommon.Resources.task_separate_32.png";
                case "Sieve":
                    return "DataCenterCommon.Resources.task_sieve_32.png";
                case "Wash":
                    return "DataCenterCommon.Resources.task_wash_32.png";
                case "WetMill":
                    return "DataCenterCommon.Resources.task_wet_mill_32.png";
                case "ControlPH":
                    return "DataCenterCommon.Resources.task_ph_loop_32.png";
                case "DoseAtRate":
                    return "DataCenterCommon.Resources.task_dose_32.png";
                case "Distill":
                    return "DataCenterCommon.Resources.task_distill_32.png";
                case "HeatCool":
                    return "DataCenterCommon.Resources.task_heat_cool_32.png";
                case "AddAtOnce":
                    return "DataCenterCommon.Resources.task_manual_add_32.png";
                case "OperatorMessage":
                    return "DataCenterCommon.Resources.task_message_32.png";
                case "Reflux":
                    return "DataCenterCommon.Resources.task_reflux_32.png";
                case "Settle":
                    return "DataCenterCommon.Resources.task_settle_32.png";
                case "Stir":
                    return "DataCenterCommon.Resources.task_stir_32.png";
                case "TakeSample":
                    return "DataCenterCommon.Resources.task_take_sample_32.png";
                case "Wait":
                    return "DataCenterCommon.Resources.task_wait_32.png";
                default:
                    return null;
            }
        }
    }
}
