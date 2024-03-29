﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using middlerApp.IDP.DataAccess.Entities.Entities;
using middlerApp.IDP.Library.DtoModels;

namespace middlerApp.IDP.Library.Mappers
{
    /// <summary>
    /// Defines entity/model mapping for scopes.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class ScopeMapperProfile : Profile
    {
        /// <summary>
        /// <see cref="ScopeMapperProfile"/>
        /// </summary>
        public ScopeMapperProfile()
        {
            CreateMap<ScopeProperty, KeyValuePair<string, string>>()
                .ReverseMap();

            CreateMap<ScopeClaim, string>()
               .ConstructUsing(x => x.Type)
               .ReverseMap()
               .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src));

            CreateMap<Scope, IdentityServer4.Models.ApiScope>(MemberList.Destination)
                .ConstructUsing(src => new IdentityServer4.Models.ApiScope())
                .ForMember(x => x.Properties, opts => opts.MapFrom(x => x.Properties))
                .ForMember(x => x.UserClaims, opts => opts.MapFrom(x => x.UserClaims.Select(u => u.Scope.Name)))
                .ReverseMap();


            CreateMap<Scope, MScopeDto>();
        }
    }
}
