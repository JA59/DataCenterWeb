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
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using DataCenterCommon.ViewModels;
using DataCenterCommon.Interfaces;

namespace DataCenterWebApp.Controllers
{
    [Route("api/[controller]")]
    public class PlannedExperimentController : BaseApiController
    {
        private readonly ILogger<PlannedExperimentController> _logger;

        #region Constructor
        public PlannedExperimentController(
            RoleManager<MyRole> roleManager,
            UserManager<MyUser> userManager,
            IConfiguration configuration,
            IDataCenterLib dataCenterLib,
            ILogger<PlannedExperimentController> logger
            ) : base(roleManager, userManager, configuration, dataCenterLib)
        {
            _logger = logger;
        }
        #endregion

        #region RESTful methods
        /// <summary>
        /// GET: api/plannedexperiment/{id}
        /// Retrieves the Planned Experiment for the given {id}
        /// </summary>
        /// <param name="id">The ID of an existing Planned Experiment</param>
        /// <returns>PlannedExperimentViewModel for the given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            _logger.LogInformation("Get");
            if (!HasRole("user"))
            {
                return new UnauthorizedResult();
            }

            // create a sample plannedexperiment to match the given request
            //var plannedExperiment = DataCenterCache.Instance.PlannedExperiments.Where(t => t.TrackingId == id).FirstOrDefault();
            var plannedExperiment = DataCenterLib.GetPlannedExperiment(id);

            // output the result in JSON format
            return new JsonResult(
                plannedExperiment,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// GET: api/plannedexperiment/svg/{id}
        /// Retrieves the SVG graphic for the Planned Experiment with the given {id}
        /// </summary>
        /// <param name="id">The ID of an existing Planned Experiment</param>
        /// <returns>SVG graphic</returns>
        [HttpGet("svg/{id}")]
        public IActionResult GetSvg(string id)
        {
            _logger.LogInformation("GetSvg");
            var svg = DataCenterLib.GetPlannedExperimentAsSvg(id);

            // output the result in JSON format
            return new ContentResult()
            {
                Content = svg,
                ContentType = "image/svg+xml",
            };
        }

        /// <summary>
        /// Adds a new Planned Experiment to the Database
        /// PUT: api/plannedexperiment/{id}
        /// </summary>
        /// <param name="model">The Planned ExperimentViewModel containing the data to insert</param>
        [HttpPut]
        public IActionResult Put(ElnExperiment model)
        {
            // We don't support creating new planned experiments
            throw new NotImplementedException();
        }

        /// <summary>
        /// Edit the Planned Experiment with the given {id}
        /// POST: api/plannedexperiment/{id}
        /// </summary>
        /// <param name="model">The Planned ExperimentViewModel containing the data to update</param>
        [HttpPost]
        public IActionResult Post(ElnExperiment model)
        {
            // We don't support updating planned experiments
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the Planned Experiment with the given {id} from the Database
        /// DELETE: api/plannedexperiment/{id}
        /// </summary>
        /// <param name="id">The ID of an existing planned experiment</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _logger.LogInformation("Delete");
            if (!HasRole("admin"))
            {
                return new UnauthorizedResult();
            }

            DataCenterLib.DeleteExperiment(id);

            // output the result in JSON format
            return new JsonResult(
                id,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// GET: api/plannedexperiment/ByPage/{page}
        /// Retrieves the specified page of planned experiments
        /// </summary>
        /// <param name="pge">the number of planned experiments to retrieve</param>
        /// <returns>{num} Planned Experiments sorted by User</returns>
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("ByPage/{pge:int?}")]
        public IActionResult ByPage(int pge = 1)
        {
            _logger.LogInformation("ByPage");
            if (!HasRole("user"))
            {
                return new UnauthorizedResult();
            }

            int pageSize = 20;
            //var plannedExperiments = DataCenterCache.Instance.PlannedExperiments;
            var plannedExperiments = DataCenterLib.GetAllPlannedExperiments();

            var result = new PagedResult<ElnExperiment>();
            result.CurrentPage = pge;
            result.PageSize = pageSize;
            result.RowCount = plannedExperiments.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (pge - 1) * pageSize;
            result.Results = plannedExperiments.Skip(skip).Take(pageSize).ToList();

            return new JsonResult(
                result,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// GET: api/plannedexperiment/ByAge
        /// Retrieves all planned experiments sorted by age
        /// </summary>
        /// <returns>{num} Planned Experiments sorted by age</returns>
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("ByAge")]
        public IActionResult ByAge()
        {
            _logger.LogInformation("ByAge");
            if (!HasRole("user"))
            {
                return new UnauthorizedResult();
            }

            //var plannedExperiments = DataCenterCache.Instance.PlannedExperiments;//dc.GetAllPlannedExperiments();
            var plannedExperiments = DataCenterLib.GetAllPlannedExperiments();

            return new JsonResult(
                plannedExperiments,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        #endregion

        private bool HasRole(string role)
        {
            //return true;
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                MyUser myUser = UserManager.FindByIdAsync(userId).Result;
                return myUser.Roles.Contains(role);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

