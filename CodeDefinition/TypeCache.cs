using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JintTsDefinition
{
    internal static class TypeCache
    {
        public static ConcurrentDictionary<string, TypeDefinition> Cache { get; } = new ConcurrentDictionary<string, TypeDefinition>();


        public static TypeDefinition TDNull { get; } = new TypeDefinition
        {
            Name = "NULL"
        };

        
    }
}