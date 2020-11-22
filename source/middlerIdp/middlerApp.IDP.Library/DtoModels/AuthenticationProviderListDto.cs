using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using middlerApp.IDP.DataAccess.Entities.Entities;

namespace middlerApp.IDP.Library.DtoModels
{
    public class AuthenticationProviderListDto
    {
        public Guid Id { get; set; }

        public string Type { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}
