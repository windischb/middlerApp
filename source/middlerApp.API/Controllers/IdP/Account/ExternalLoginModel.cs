using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace middlerApp.API.Controllers.IdP.Account
{
    public class ExternalLoginModel
    {
        public string Scheme { get; set; }
        public string ReturnUrl { get; set; }
    }
}
