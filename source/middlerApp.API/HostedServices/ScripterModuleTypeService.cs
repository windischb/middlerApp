using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Prise;

namespace middlerApp.API.HostedServices
{
    public class ScripterModuleTypeService: IHostedService
    {
        private readonly IPluginLoader _pluginLoader;

        public ScripterModuleTypeService(IPluginLoader pluginLoader)
        {
            _pluginLoader = pluginLoader;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
