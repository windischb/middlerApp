using AutoMapper;
using middler.Common.SharedModels.Models;
using middlerApp.API.Helper;
using middlerApp.API.MapperProfiles.Formatters;
using middlerApp.Core.DataAccess.Entities.Models;
using middlerApp.SharedModels;

namespace middlerApp.API.MapperProfiles
{
    public class EndpointActionProfile: Profile
    {
        public EndpointActionProfile()
        {
            CreateMap(typeof(EndpointActionEntity), typeof(MiddlerAction<>))
                .ForMember("Parameters", mbr => mbr.ConvertUsing(new StringToJObjectFormatter(), "Parameters"));

            CreateMap(typeof(MiddlerAction<>), typeof(EndpointActionEntity))
                .ForMember("Parameters", mbr => mbr.ConvertUsing(new StringToJObjectFormatter(), "Parameters"));

            CreateMap<EndpointActionEntity, MiddlerAction>();

            CreateMap<EndpointActionEntity, EndpointActionDto>()
                .ForMember(dest => dest.Parameters, expression => expression.MapFrom(dto => Converter.Json.ToDictionary(dto.Parameters)));

            CreateMap<EndpointActionDto, EndpointActionEntity>()
                .ForMember(dest => dest.Parameters, expression => expression.MapFrom(dto => Converter.Json.ToJObject(dto.Parameters)));

            CreateMap<EndpointActionEntity, EndpointRuleListActionDto>();

            CreateMap<EndpointActionEntity, MiddlerAction>();
        }
    }
}
