using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenterLib
{
    public class SystemOverview
    {
        public string LastImportDate { get; set; }
        public int ExperimentCount { get; set; }
        public int HighestSequenceID { get; set; }
        public string ICDataCenterAddress { get; set; }
        public string ICDataCenterVersion { get; set; }
        public string ICDataCenterStatus { get; set; }
        public string DataCenterWebAppAddress { get; set; }
        public string DataCenterWebAppVersion { get; set; }
        public string DataCenterWebAppStatus { get; set; }
        public string LastUpdate { get; set; }
    }
}
