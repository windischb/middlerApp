using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reflectensions.ExtensionMethods;


namespace LdapTools
{
    public class LdapMod
    {

        public AttributeInfo AttributeInfo { get; }

        public object Value { get; internal set; }

        public bool ValueIsArray { get; }


        
        public LdapMod(AttributeInfo attributeInfo, object value)
        {
            AttributeInfo = attributeInfo;
            Value = value;
            ValueIsArray = !(value is byte[]) && value.GetType().IsEnumerableType();

        }

        public IEnumerable<T> ValueAsEnumerableOf<T>()
        {
            return ValueIsArray ? Value.To<List<object>>().Select(v => v.To<T>()) : new List<T>{Value.To<T>()};
        }

        
        //public DirectoryAttributeModification BuildAttributeModification(DirectoryAttributeOperation operation)
        //{
        //    var mod = new DirectoryAttributeModification();
        //    mod.Name = AttributeInfo.Name;
        //    mod.Operation = operation;


        //}


        //private void AddModificationValue(DirectoryAttributeModification modification)
        //{
        //    switch (AttributeInfo.LDAPSyntax)
        //    {
        //        case "Boolean":
        //        {
        //            modification.Add(Value.ToString());
        //            break;
        //        }
        //        case "Enumeration":
        //        {

        //            break;
        //        }
                
        //    }
        //}

        
    }


   
}
