﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using middlerApp.API.IDP.DtoModels;
using middlerApp.API.IDP.Models;

namespace middlerApp.API.IDP.Mappers
{
    public class MRoleMapperProfile : Profile
    {
        public MRoleMapperProfile()
        {
            CreateMap<MRole, MRoleDto>()
                .ForMember(dest => dest.Users, expression => expression.MapFrom((role, dto) => role.Users));

            CreateMap<MRoleDto, MRole>()
                .ForMember(dest => dest.Users, expression => expression.MapFrom((dto, role) => dto.Users));


            CreateMap<MRole, MRoleListDto>();
        }
    }
}
