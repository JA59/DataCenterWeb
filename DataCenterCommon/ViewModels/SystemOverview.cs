using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenterCommon.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class SystemOverview
    {
        #region Constructor
        public SystemOverview()
        {

        }
        #endregion

        #region Properties
        public DateTime LastImportDate { get; set; }
        public int ExperimentCount { get; set; }
        public int HighestSequenceID { get; set; }
        public string ICDataCenterAddress { get; set; }
        public string ICDataCenterVersion { get; set; }
        public string ICDataCenterStatus { get; set; }
        public string DataCenterWebAppAddress { get; set; }
        public string DataCenterWebAppVersion { get; set; }
        public string DataCenterWebAppStatus { get; set; }
        public DateTime LastUpdate { get; set; }
        #endregion
    }
}
