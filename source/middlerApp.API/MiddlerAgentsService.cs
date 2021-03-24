using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using middlerApp.Agents.Shared;
using middlerApp.Agents.Shared.ExtensionMethods;
using middlerApp.API.ExtensionMethods;
using Reflectensions.Helper;
using SignalARRR.Server;

namespace middlerApp.API
{
    public class MiddlerAgentsService : IMiddlerAgentsService
    {
        private readonly ClientManager _clientManager;

        public MiddlerAgentsService(ClientManager clientManager)
        {
            _clientManager = clientManager;
        }

        public List<IMiddlerAgent> GetAllAgents()
        {
            var clients = _clientManager.GetHARRRClients<RemoteAgentHub>();
            return clients.Select(c => new MiddlerAgent(c)).Cast<IMiddlerAgent>().ToList();
        }

        public IMiddlerAgent GetRandomAgent()
        {
            return GetAllAgents().RandomSingle();
        }
    }


    public class MiddlerAgent: IMiddlerAgent
    {
        private ClientContext _clientContext;
        public string Identifier { get; }

        private IEnumerable<Type> _implementsInterface { get; }

        public MiddlerAgent(ClientContext clientContext)
        {
            _clientContext = clientContext;
            if (clientContext.Attributes.TryGetValue("Implements", out var implements))
            {
                _implementsInterface = implements.Select(TypeHelper.FindType).Where(t => t != null);
            }
        }

        public T GetInterface<T>() where T : class
        {
            return _clientContext.GetTypedMethods<T>(IScripterContextExtensions.assemblyLocations);
        }


        public bool ImplementsInterface<T>()
        {
            return ImplementsInterface(typeof(T));
        }

        public bool ImplementsInterface(Type interfaceType)
        {
            return _implementsInterface.Contains(interfaceType);
        }
    }
    
}
