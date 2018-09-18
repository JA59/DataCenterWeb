using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenterLib
{
    [JsonObject(MemberSerialization.OptOut)]
    public class PlannedExperimentViewModel
    {
        #region Constructor
        public PlannedExperimentViewModel()
        {

        }
        #endregion

        #region Properties
        //public string Id { get; set; }
        public string TrackingId { get; set; }
        public string ExperimentName { get; set; }
        public string Project { get; set; }
        public string User { get; set; }
        public DateTime CreatedTime { get; set; }
        public string SchemaVersion { get; set; }
        public string ProcessType { get; set; }
        public string UniqueElnId { get; set; }
        public string Svg { get; set; }

        #endregion
    }
}
