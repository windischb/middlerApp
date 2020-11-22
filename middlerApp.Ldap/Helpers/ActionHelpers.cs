using System;

namespace LdapTools.Helpers
{
    public static class ActionHelpers
    {
        public static T InvokeAction<T>(Action<T> action, T instance = default)
        {
            if (instance == null)
            {
                instance = Activator.CreateInstance<T>();
            }

            action(instance);
            return instance;
        }

        
    }
}
