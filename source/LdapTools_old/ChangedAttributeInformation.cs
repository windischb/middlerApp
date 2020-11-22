using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdapTools
{
    public class ChangedAttributeInformation
    {
        public string Name { get; }
        public ChangedAttributeType Type { get; }

        public object OldValue { get; }
        public object NewValue { get; }

        public ChangedAttributeInformation(string name, ChangedAttributeType type)
        {
            Name = name;
            Type = type;
        }

        public ChangedAttributeInformation(string name, ChangedAttributeType type, object newValue, object oldValue)
        {
            Name = name;
            Type = type;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    public enum ChangedAttributeType
    {
        Added,
        Changed,
        Removed,
        Set,
        AttributeDoesntExist
    }
}
