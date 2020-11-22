using System;
using LdapTools.Helpers;

namespace LdapTools
{
    
        public static class ActionExtensions
        {
            public static T InvokeAction<T>(this Action<T> action, T instance = default) => ActionHelpers.InvokeAction(action, instance);
        }
    
}
