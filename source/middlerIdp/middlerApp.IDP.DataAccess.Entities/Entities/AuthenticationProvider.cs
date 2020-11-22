using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace middlerApp.IDP.DataAccess.Entities.Entities
{
    public class AuthenticationProvider
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public JObject Parameters { get; set; } = new JObject();

    }
    
}
