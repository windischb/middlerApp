using AutoMapper;
using middlerApp.IDP.DataAccess.Entities.Entities;

namespace middlerApp.IDP.Library.Mappers
{
    public class AuthorizationCodeMapperProfile: Profile
    {
        public AuthorizationCodeMapperProfile()
        {
            CreateMap<AuthorizationCode, IdentityServer4.Models.AuthorizationCode>()
                .ReverseMap();
        }
    }
}
