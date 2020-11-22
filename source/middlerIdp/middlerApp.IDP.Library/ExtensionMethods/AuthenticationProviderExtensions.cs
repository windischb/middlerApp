using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using middlerApp.IDP.DataAccess.Entities.Entities;
using middlerApp.IDP.Library.DtoModels;
using Reflectensions.ExtensionMethods;

namespace middlerApp.IDP.Library.ExtensionMethods
{
    public static class AuthenticationProviderExtensions
    {
        public static AuthenticationProviderListDto ToListDto(this AuthenticationProvider authenticationProvider)
        {
            var dto = new AuthenticationProviderListDto();
            dto.Id = authenticationProvider.Id;
            dto.Name = authenticationProvider.Name;
            dto.DisplayName = authenticationProvider.DisplayName;
            dto.Description = authenticationProvider.Description;
            dto.Type = authenticationProvider.Type;

            return dto;
        }

        public static AuthenticationProvider Map(this AuthenticationProvider provider,
            AuthenticationProvider authenticationProvider)
        {
            provider.Name = authenticationProvider.Name;
            provider.DisplayName = authenticationProvider.DisplayName;
            provider.Description = authenticationProvider.Description;
            provider.Enabled = authenticationProvider.Enabled;
            provider.Type = authenticationProvider.Type;

            return provider;
        }
    }
}
