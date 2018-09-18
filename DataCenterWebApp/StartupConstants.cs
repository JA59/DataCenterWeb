using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCenterWebApp
{
    public static class StartupConstants
    {
        // Adjust the following constants to set simulation and run mode 
        private const bool _useSimulation = true;
        private const RunMode _RunMode = RunMode.WindowsApp;

        /// <summary>
        /// RunMode
        /// </summary>
        public static RunMode RunMode = _RunMode;

        /// <summary>
        /// True if using simulated iC Data Center
        /// </summary>
        public static bool UsingSimulation = _useSimulation || RunMode == RunMode.IoT || RunMode == RunMode.Azure;
    }

    /// <summary>
    /// Run modes
    /// </summary>
    public enum RunMode
    {
        WindowsApp,
        Service,
        Azure,
        IoT
    }
}
