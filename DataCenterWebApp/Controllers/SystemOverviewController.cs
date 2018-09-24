using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using DataCenterCommon.Interfaces;
using DataCenterWebApp.CustomIdentity;
using DataCenterWebApp.ViewModels;

namespace DataCenterWebApp.Controllers
{
    /// <summary>
    /// Controller for handling the SystemOverview route
    /// </summary>
    [Route("api/[controller]")]
    public class SystemOverviewController : BaseApiController
    {
        private readonly ILogger<SystemOverviewController> _logger;
        #region Constructor
        public SystemOverviewController(
            RoleManager<MyRole> roleManager,            // role manager - ued to obtain roles for the currently logged on user
            UserManager<MyUser> userManager,            // user manager - used to obtain the currently logged on user 
            IConfiguration configuration,               // configuration - not used (needed to construct base class)
            IDataCenterLib dataCenterLib,               // interface to iC Data Center (live or simulated)
            ILogger<SystemOverviewController> logger    // logger (not in base class because it is for type SystemOverviewController)

            )  : base(roleManager, userManager, configuration, dataCenterLib)
        {
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// GET: api/SystemOverview/Summary}
        /// Retrieves the specified page of planned experiments
        /// </summary>
        /// <param name="num">the number of planned experiments to retrieve</param>
        /// <returns>{num} Planned Experiments sorted by User</returns>
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("Summary")]
        public IActionResult Summary()
        {
            try
            {
                // Log that we were called.
                _logger.LogInformation("Summary");

                // Get a SystemOverview object from the IDataCenterLib object
                var systemOverview = DataCenterLib.GetSystemOverview();
                
                // Construct a SystemOverviewViewModel object from the SystemOverview object
                var systemOverviewViewModel = new SystemOverviewViewModel()
                {
                    LastImportDate = systemOverview.LastImportDate,
                    ExperimentCount = systemOverview.ExperimentCount,
                    HighestSequenceID = systemOverview.HighestSequenceID,
                    ICDataCenterAddress = systemOverview.ICDataCenterAddress,
                    ICDataCenterVersion = systemOverview.ICDataCenterVersion,
                    ICDataCenterStatus = systemOverview.ICDataCenterStatus,
                    DataCenterWebAppAddress = systemOverview.DataCenterWebAppAddress,
                    DataCenterWebAppVersion = systemOverview.DataCenterWebAppVersion,
                    DataCenterWebAppStatus = systemOverview.DataCenterWebAppStatus,
                    LastUpdate = systemOverview.LastUpdate,
                    LoggedOnUser = GetUser(),
                    LoggedOnRole = GetRoles()
                };

                // Return the SystemOverviewViewModel in JSON format
                return new JsonResult(
                    systemOverviewViewModel,
                    new JsonSerializerSettings()
                    {
                        Formatting = Formatting.Indented
                    });
            }
            catch (Exception exc)
            {
                // Log the exception and return it to the caller
                _logger.LogError(exc.ToString());
                throw;
            }
        }

        /// <summary>
        /// Get a string that contains a comma separated set of roles for the current user (based on the ClaimsPrincipal)
        /// </summary>
        /// <returns></returns>
        private string GetRoles()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                MyUser myUser = UserManager.FindByIdAsync(userId).Result;
                string roles = String.Empty;
                foreach (string role in myUser.Roles)
                    roles = roles + role + ", ";
                return roles.Substring(0, roles.Length - 2);
            }
            catch (Exception)
            {
                return "<none>";
            }
        }

        /// <summary>
        /// Get a string that represents the currently logged on user (based on the ClaimsPrincipal)
        /// </summary>
        /// <returns></returns>
        private string GetUser()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                MyUser myUser = UserManager.FindByIdAsync(userId).Result;
                return myUser.UserName;
            }
            catch (Exception)
            {
                return "Guest";
            }
        }
    }
}

