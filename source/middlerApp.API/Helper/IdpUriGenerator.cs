using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace middlerApp.API.Helper
{
    public static class IdpUriGenerator
    {
        public static string GenerateRedirectUri(string ipAddress, int port)
        {
            var idpListenIp = IPAddress.Parse(ipAddress);
            var isLocalhost = IPAddress.IsLoopback(idpListenIp) || idpListenIp.ToString() == IPAddress.Any.ToString();

            if (isLocalhost)
            {
                return port == 443 ? $"https://localhost" : $"https://localhost:{port}";
            }
            else
            {
                return port == 443
                    ? $"https://{ipAddress}"
                    : $"https://{ipAddress}:{port}";
            }
        }

    }
}
