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
    [Route("api/identity-ui")]
    [IdPController]
    public class IdentityUIConfigController: Controller
    {
        private readonly StartUpConfiguration _startUpConfiguration;

        public IdentityUIConfigController(StartUpConfiguration startUpConfiguration)
        {
            _startUpConfiguration = startUpConfiguration;
        }


        [HttpGet("config")]
        public IActionResult Get()
        {
            var conf = new IdentityUIConfig();

            conf.IDPBaseUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Host}:{_startUpConfiguration.IdpSettings.HttpsPort}";

            return Ok(conf);
        }
    }
}
