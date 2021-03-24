using System;
using System.Collections.Generic;
using System.Reflection;

namespace middlerApp.Agents.Shared
{
    public interface IMiddlerAgent
    {
        string Identifier { get; }

        T GetInterface<T>() where T : class;
        T GetInterface<T>(IEnumerable<Assembly> assemblies) where T : class;
        bool ImplementsInterface(Type interfaceType);
        bool ImplementsInterface<T>();
    }
}