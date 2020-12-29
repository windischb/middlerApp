using System;

namespace middlerApp.Agents.Shared
{
    public interface IMiddlerAgent
    {
        string Identifier { get; }

        T GetInterface<T>() where T : class;
        bool ImplementsInterface(Type interfaceType);
        bool ImplementsInterface<T>();
    }
}