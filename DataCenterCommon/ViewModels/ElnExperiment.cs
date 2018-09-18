using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenterCommon.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ElnExperiment
    {
        #region Constructor
        public ElnExperiment()
        {

        }
        #endregion

        #region Properties
        public string TrackingId { get; set; }
        public string ExperimentName { get; set; }
        public string Project { get; set; }
        public string User { get; set; }
        public DateTime CreatedTime { get; set; }
        public string SchemaVersion { get; set; }
        public string ProcessType { get; set; }
        public string UniqueElnId { get; set; }
        public string Svg { get; set; }
        public bool Selected { get; set; }

        #endregion
    }
}

