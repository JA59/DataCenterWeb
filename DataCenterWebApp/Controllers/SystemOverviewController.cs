using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using DataCenterWebApp.CustomIdentity;
using Microsoft.Extensions.Configuration;
using DataCenterWebApp.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.Extensions.Logging;
using DataCenterCommon.Interfaces;

namespace DataCenterWebApp.Controllers
{
    [Route("api/[controller]")]
    public class SystemOverviewController : BaseApiController
    {
        private readonly ILogger<SystemOverviewController> _logger;
        #region Constructor
        public SystemOverviewController(
            RoleManager<MyRole> roleManager,
            UserManager<MyUser> userManager,
            IConfiguration configuration,
            IDataCenterLib dataCenterLib,
            ILogger<SystemOverviewController> logger

            )  : base(roleManager, userManager, configuration, dataCenterLib)
        {
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// GET: api/overview/Summary}
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
                _logger.LogInformation("Summary");
                // Use the summary from the cache, filling in the current user and roles
                //var so = DataCenterCache.Instance.SystemOverview;
                var so = DataCenterLib.GetSystemOverview();
                var result = new SystemOverviewViewModel()
                {
                    LastImportDate = so.LastImportDate,
                    ExperimentCount = so.ExperimentCount,
                    HighestSequenceID = so.HighestSequenceID,
                    ICDataCenterAddress = so.ICDataCenterAddress,
                    ICDataCenterVersion = so.ICDataCenterVersion,
                    ICDataCenterStatus = so.ICDataCenterStatus,
                    DataCenterWebAppAddress = so.DataCenterWebAppAddress,
                    DataCenterWebAppVersion = so.DataCenterWebAppVersion,
                    DataCenterWebAppStatus = so.DataCenterWebAppStatus,
                    LastUpdate = so.LastUpdate,
                    LoggedOnUser = GetUser(),
                    LoggedOnRole = GetRoles()
                };  
                
                return new JsonResult(
                    result,
                    new JsonSerializerSettings()
                    {
                        Formatting = Formatting.Indented
                    });
            }
            catch (Exception exc)
            {
                var a = exc;
                throw;
            }
        }

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

