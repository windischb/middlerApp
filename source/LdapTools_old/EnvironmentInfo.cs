using System;
using System.Net.NetworkInformation;

namespace LdapTools
{
    public class EnvironmentInfo
    {

        private static EnvironmentInfo _environmentInfo;
        public static EnvironmentInfo Instance => _environmentInfo ?? (_environmentInfo = new EnvironmentInfo());
        
        public string DomainName { get; }
        
        private EnvironmentInfo()
        {
            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            DomainName = ipGlobalProperties.DomainName;
        }
    }
}
