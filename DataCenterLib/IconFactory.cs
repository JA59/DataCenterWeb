using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace DataCenterLib
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
                    return "DataCenterLib.Resources.task_centrifuge_32.png";
                case "Discharge":
                    return "DataCenterLib.Resources.task_discharge_32.png";
                case "Dry":
                    return "DataCenterLib.Resources.task_dry_32.png";
                case "Filter":
                    return "DataCenterLib.Resources.task_filter_32.png";
                case "Homogenize":
                    return "DataCenterLib.Resources.task_homigenize_32.png";
                case "Inert":
                    return "DataCenterLib.Resources.task_inert_32.png";
                case "Mill":
                    return "DataCenterLib.Resources.task_mill_32.png";
                case "PackAndLabel":
                    return "DataCenterLib.Resources.task_pack_label_32.png";
                case "PressureAdjust":
                    return "DataCenterLib.Resources.task_pressure_32.png";
                case "Recirculate":
                    return "DataCenterLib.Resources.task_recirculate_32.png";
                case "Rinse":
                    return "DataCenterLib.Resources.task_rinse_32.png";
                case "Separate":
                    return "DataCenterLib.Resources.task_separate_32.png";
                case "Sieve":
                    return "DataCenterLib.Resources.task_sieve_32.png";
                case "Wash":
                    return "DataCenterLib.Resources.task_wash_32.png";
                case "WetMill":
                    return "DataCenterLib.Resources.task_wet_mill_32.png";
                case "ControlPH":
                    return "DataCenterLib.Resources.task_ph_loop_32.png";
                case "DoseAtRate":
                    return "DataCenterLib.Resources.task_dose_32.png";
                case "Distill":
                    return "DataCenterLib.Resources.task_distill_32.png";
                case "HeatCool":
                    return "DataCenterLib.Resources.task_heat_cool_32.png";
                case "AddAtOnce":
                    return "DataCenterLib.Resources.task_manual_add_32.png";
                case "OperatorMessage":
                    return "DataCenterLib.Resources.task_message_32.png";
                case "Reflux":
                    return "DataCenterLib.Resources.task_reflux_32.png";
                case "Settle":
                    return "DataCenterLib.Resources.task_settle_32.png";
                case "Stir":
                    return "DataCenterLib.Resources.task_stir_32.png";
                case "TakeSample":
                    return "DataCenterLib.Resources.task_take_sample_32.png";
                case "Wait":
                    return "DataCenterLib.Resources.task_wait_32.png";
                default:
                    return null;
            }
        }
    }
}
