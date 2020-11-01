using System;
using System.Collections.Generic;
using System.Text;

namespace middlerApp.IDP.Library
{
    public class IdpConfiguration
    {
        public List<string> AdminUIRedirectUris { get; set; } = new List<string>();
        public List<string> AdminUIPostLogoutUris { get; set; } = new List<string>();
    }
}
