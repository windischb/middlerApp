using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using middlerApp.Agents.Shared;
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


        public T GetInterface<T>() where T: class
        {
            var clients = _clientManager.GetHARRRClients<RemoteAgentHub>();
            var client = clients.FirstOrDefault(agent => agent.Attributes.Has("Implements", typeof(T).FullName));

            return client.GetTypedMethods<T>();
        }
    }

    
}
