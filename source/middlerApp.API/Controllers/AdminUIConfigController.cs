using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc;
using middlerApp.API.Attributes;
using middlerApp.API.Models;

namespace middlerApp.API.Controllers
{
    [Route("api/admin-ui")]
    [AdminController]
    public class AdminUIConfigController: Controller
    {
        private readonly StartUpConfiguration _startUpConfiguration;

        public AdminUIConfigController(StartUpConfiguration startUpConfiguration)
        {
            _startUpConfiguration = startUpConfiguration;
        }


        [HttpGet("config")]
        public async Task<IActionResult> Get()
        {
            var conf = new AdminUIConfig();

            conf.IDPBaseUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Host}:{_startUpConfiguration.IdpSettings.HttpsPort}";

            return Ok(conf);
        }
    }
}
