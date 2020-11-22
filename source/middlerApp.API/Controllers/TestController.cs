using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using middlerApp.API.Attributes;
using SignalARRR.Server;
using SignalARRR.Server.ExtensionMethods;

namespace middlerApp.API.Controllers
{
    [Route("api/test")]
    [AdminController]
    public class TestController: Controller
    {
        private ClientManager ClientManager { get; }

        public TestController(ClientManager clientManager)
        {
            ClientManager = clientManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var result = await ClientManager.GetAllClients().InvokeAllAsync<string>("Test", new object[] {"abc", 123}, CancellationToken.None);

            return Ok(result);
        }

        [HttpGet("clients")]
        public async Task<IActionResult> GetMiddlerAgents()
        {

            var clients = ClientManager.GetHARRRClients<RemoteAgentHub>().Select(c => new MiddlerClientDto(c));
            return Ok(clients);
        }
    }

    public class MiddlerClientDto
    {
        public ClientAttributes Attributes { get; }
        public IPAddress RemoteIp { get; }

        public string User { get; set; }
        
        public DateTime ConnectedAt { get; internal set; }

        public List<DateTime> ReconnectedAt { get; } = new List<DateTime>();
        
        public Uri ConnectedTo { get; }

        public MiddlerClientDto(ClientContext context)
        {
            RemoteIp = context.RemoteIp;
            User = context.User?.Identity?.Name;
            ConnectedAt = context.ConnectedAt;
            ReconnectedAt = context.ReconnectedAt;
            ConnectedTo = context.ConnectedTo;
            Attributes = context.Attributes;
        }
    }
}
