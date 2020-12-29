using System;
using System.Collections.Generic;

namespace middlerApp.Agents.Shared
{
    public interface IMiddlerAgentsService
    {
        List<IMiddlerAgent> GetAllAgents();

        IMiddlerAgent GetRandomAgent();

        
    }
}