// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using AutoMapper;
using IdentityModel;
using IdentityServer4.Models;
using middlerApp.IDP.DataAccess.Entities.Entities;
using middlerApp.IDP.Library.DtoModels;
using Secret = middlerApp.IDP.DataAccess.Entities.Entities.Secret;

namespace middlerApp.IDP.Library.Mappers
{

    public class SecretMapperProfile : Profile
    {

        public SecretMapperProfile()
        {
            CreateMap<Secret, SecretDto>();

            CreateMap<SecretDto, ApiResourceSecret>();

            CreateMap<SecretDto, ClientSecret>();
        }
    }
}
