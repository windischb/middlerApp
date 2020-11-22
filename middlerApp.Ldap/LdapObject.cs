using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Reflectensions.HelperClasses;

namespace LdapTools
{
    public class LdapObject: ExpandableObject
    {
        public string DistinguishedName { get; set; }

        public string[] ObjectClass { get; set; }


        public LdapObject(Dictionary<string, object> properties): base(properties)
        {
            
        }


      
    }
}
