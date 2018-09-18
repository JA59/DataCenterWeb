﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCenterCommon.Interfaces;
using DataCenterWebApp.CustomIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DataCenterWebApp.Controllers
{
    [Route("api/[controller]")]
    public class BaseApiController : Controller
    {
        #region Constructor
        public BaseApiController(
            RoleManager<MyRole> roleManager,
            UserManager<MyUser> userManager,
            IConfiguration configuration,
            IDataCenterLib dataCenterLib
            )
        {
            // Instantiate the required classes through DI
            RoleManager = roleManager;
            UserManager = userManager;
            Configuration = configuration;
            DataCenterLib = dataCenterLib;

            // Instantiate a single JsonSerializerSettings object
            // that can be reused multiple times.
            JsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };

        }
        #endregion

        #region Shared Properties
        protected RoleManager<MyRole> RoleManager { get; private set; }
        protected UserManager<MyUser> UserManager { get; private set; }
        protected IConfiguration Configuration { get; private set; }
        protected JsonSerializerSettings JsonSettings { get; private set; }
        protected IDataCenterLib DataCenterLib { get; private set; }
        #endregion
    }
}
