// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Linq;
using AutoMapper;
using middlerApp.IDP.DataAccess.Entities.Entities;
using middlerApp.IDP.Library.DtoModels;

namespace middlerApp.IDP.Library.Mappers
{
    /// <summary>
    /// Defines entity/model mapping for identity resources.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class IdentityResourceMapperProfile : Profile
    {
        /// <summary>
        /// <see cref="IdentityResourceMapperProfile"/>
        /// </summary>
        public IdentityResourceMapperProfile()
        {
            //CreateMap<Storage.Entities.IdentityResourceProperty, KeyValuePair<string, string>>()
            //    .ReverseMap();

            CreateMap<Scope, IdentityServer4.Models.IdentityResource>(MemberList.Destination)
                .ForMember(dest => dest.UserClaims, opts => opts.MapFrom(x => x.UserClaims.Select(u => u.Type)))
                .ConstructUsing(src => new IdentityServer4.Models.IdentityResource())
                .ReverseMap();

            //CreateMap<Storage.Entities.IdentityResourceClaim, string>()
            //   .ConstructUsing(x => x.Type)
            //   .ReverseMap()
            //   .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src));

            CreateMap<Scope, MScopeListDto>();

            CreateMap<Scope, MScopeDto>();

            CreateMap<MScopeDto, Scope>();
        }
    }
}
