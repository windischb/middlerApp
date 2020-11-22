using System;
using System.Linq;
using AutoMapper;
using middlerApp.IDP.DataAccess.Entities.Models;
using middlerApp.IDP.Library.DtoModels;

namespace middlerApp.IDP.Library.Mappers
{
    public class MUserMapperProfile : Profile
    {
        public MUserMapperProfile()
        {
            CreateMap<MUserDto, MUser>()
                .ForMember(dest => dest.Roles, expression => expression.MapFrom((dto, user) => dto.Roles.Select(r => new MRole() {Id = r.Id})));

            CreateMap<MUser, MUserDto>()
                .ForMember(dest => dest.Roles,
                    expression => expression.MapFrom((user, dto) => user.Roles))
                .ForMember(dest => dest.HasPassword,
                    expression => expression.MapFrom((user, dto) => !String.IsNullOrWhiteSpace(user.Password)));

            CreateMap<MUserClaim, MUserClaimDto>().ReverseMap();

            CreateMap<MExternalClaim, MExternalClaimDto>().ReverseMap();

            CreateMap<MUser, MUserListDto>()
                .ForMember(dest => dest.HasPassword,
                    expression => expression.MapFrom((user, dto) => !String.IsNullOrWhiteSpace(user.Password)))
                .ForMember(dest => dest.Logins, expression => expression.MapFrom((user, dto) => user.Logins.Select(l => l.Provider)));

            CreateMap<MUserListDto, MUser>();
        }
    }
}
