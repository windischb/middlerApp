using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalARRR.Server;

namespace middlerApp.API
{
    public class RemoteAgentHub : HARRR
    {
        public RemoteAgentHub(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
