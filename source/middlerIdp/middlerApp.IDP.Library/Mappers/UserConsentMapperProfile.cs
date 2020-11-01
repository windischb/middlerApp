using System;
using System.Linq;
using AutoMapper;
using IdentityServer4.Models;
using middlerApp.IDP.DataAccess.Entities.Entities;

namespace middlerApp.IDP.Library.Mappers
{
    public class UserConsentMapperProfile: Profile
    {
        public UserConsentMapperProfile()
        {
            CreateMap<UserConsent, Consent>()
                .ForMember(dest => dest.Scopes,
                    expression => expression.MapFrom((src, dest) =>
                        src.Scopes?.Split(";").Select(s => s.Trim()).Where(s => !String.IsNullOrWhiteSpace(s))));

            CreateMap<Consent, UserConsent>()
                .ForMember(dest => dest.Scopes,
                    expression => expression.MapFrom((src, dest) => String.Join(';', src.Scopes)));



        }
    }
}
