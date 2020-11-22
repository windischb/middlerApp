using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using middlerApp.API.Attributes;
using middlerApp.API.ExtensionMethods;
using middlerApp.API.Models;
using Reflectensions.ExtensionMethods;

namespace middlerApp.API.Controllers
{
    [Route("api/status")]
    [AdminController]

    public class StatusController: Controller
    {

        private static DateTime ServiceStart { get; } = Process.GetCurrentProcess().StartTime;
        private IWebHostEnvironment hostEnvironment;
        private readonly IAuthenticationSchemeProvider _schemeProvider;


        public StatusController(IWebHostEnvironment environment, IAuthenticationSchemeProvider schemeProvider)
        {
            hostEnvironment = environment;
            _schemeProvider = schemeProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatus()
        {
            //await this.HttpContext.AuthenticateAsync("Bearer");

            var isAuthenticated = this.User.Identity?.IsAuthenticated ?? false;

            var status = new Status()
                {
                    ServiceName = this.GetType().Assembly.GetName().Name,
                    CurrentDateTime = DateTime.Now,
                    ClientIp = Request.FindSourceIp().FirstOrDefault()?.ToString(),
                    Version = this.GetType().Assembly.GetName().Version.ToString(),
                    UserAgent = Request.Headers["User-Agent"].ToString(),

                    ProxyServers = Request.FindSourceIp().Skip(1).Select(ip => ip.ToString()).ToArray(),
                    User = isAuthenticated ? this.User.Identity?.Name : "Anonymous",
                    Client = this.User.Claims.GetFirstClaimValueByType("client_id"),
                    Authenticated = isAuthenticated,
                    HostName = Environment.MachineName,
                    ServiceStart = ServiceStart,
                    ServiceRunningSince = DateTime.Now - ServiceStart,
                    ContentRoot = hostEnvironment.ContentRootPath,
                    WebRoot = hostEnvironment.WebRootPath
                };

                return Ok(status);
            
        }

        [HttpGet("scheme")]
        public async Task<IActionResult> GetSchemes()
        {

            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var sch = new AuthenticationScheme("Windows", "Windows", typeof(NegotiateHandler));
            
            
            //_schemeProvider.AddScheme();

            return Ok(schemes);
        }


        
    }
}
