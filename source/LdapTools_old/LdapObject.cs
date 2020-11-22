using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LdapTools
{
    public class LdapObject
    {
        public string DistinguishedName => Properties.TryGetValue("distinguishedName", out var dn) ? dn.ToString() : null;

        public string[] ObjectClass => Properties.TryGetValue("objectClass", out var dn) ? dn as string[] : null;



        private Dictionary<string, object> _properties = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        public Dictionary<string, object> Properties
        {
            get
            {
                return _properties;
            }
            set
            {
                if (value != null)
                {
                    _properties = new Dictionary<string, object>(value, StringComparer.OrdinalIgnoreCase);
                }
                else
                {
                    _properties = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                }
            }
        }

        public LdapObject(Dictionary<string, object> properties)
        {
            Properties = properties;
        }

        public object this[string key] => Properties[key];

        public object GetPropertyOrNull(string key)
        {
            return ContainsAttribute(key) ? this[key] : null;
        }

        public bool ContainsAttribute(string attributeName)
        {
            return Properties.ContainsKey(attributeName);
        }
    }
}
