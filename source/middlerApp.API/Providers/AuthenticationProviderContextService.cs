using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using middlerApp.IDP.DataAccess.Entities.Entities;
using middlerApp.IDP.Library.Services;
using NamedServices.Microsoft.Extensions.DependencyInjection;

namespace middlerApp.API.Providers
{
    public class AuthenticationProviderContextService
    {
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IAuthenticationProviderService _authenticationProviderService;

        private readonly IServiceProvider _serviceProvider;

        private static ConcurrentDictionary<string, IAuthHandler> RegisteredHandlers { get; } = new ConcurrentDictionary<string, IAuthHandler>();


        public AuthenticationProviderContextService(
            IAuthenticationSchemeProvider schemeProvider,
            IAuthenticationProviderService authenticationProviderService,
            IServiceProvider serviceProvider
            )
        {
            _schemeProvider = schemeProvider;
            _authenticationProviderService = authenticationProviderService;
            _serviceProvider = serviceProvider;
        }
        
        public void RegisterProvider(AuthenticationProvider provider)
        {
            var handler = _serviceProvider.GetNamedService<IAuthHandler>(provider.Type);
            handler.Register(provider);
            RegisteredHandlers.TryAdd(provider.Name, handler);
        }

        public void UnRegisterProvider(string name)
        {
            if(RegisteredHandlers.TryRemove(name, out var handler))
            {
                handler.UnRegister();
            }
            
        }

        public void UpdateProvider(AuthenticationProvider provider)
        {
            if (RegisteredHandlers.TryRemove(provider.Name, out var handler))
            {
                handler.UnRegister();
            }

            if (provider.Enabled)
            {
                RegisterProvider(provider);
            }
        }

        public IAuthHandler GetHandler(string name)
        {
            return RegisteredHandlers.TryGetValue(name, out var handler) ? handler : null;
        }

        
    }
}
