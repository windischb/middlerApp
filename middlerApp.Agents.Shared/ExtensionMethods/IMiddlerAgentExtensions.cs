using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace middlerApp.Agents.Shared.ExtensionMethods
{
    public static class IMiddlerAgentExtensions
    {
        public static List<IMiddlerAgent> WhichImplementsInterface<T>(this List<IMiddlerAgent> agents)
        {
            return agents.Where(a => a.ImplementsInterface<T>()).ToList();
        }

        public static IMiddlerAgent RandomSingle(this List<IMiddlerAgent> agents)
        {
            var random = new Random();
            
            int index = random.Next(agents.Count());
            return agents[index];
        }
    }
}
