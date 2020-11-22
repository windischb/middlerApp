using System;
using System.Collections.Generic;
using System.Text;

namespace middlerApp.Core.DataAccess.Entities.Models
{
    public class TypeDefinition
    {
        public Guid? Id { get; set; }
        public string Module { get; set; }
        public string Content { get; set; }
    }
}
