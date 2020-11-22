using AutoMapper;
using AutoMapper.EquivalencyExpression;
using middler.Common.SharedModels.Models;
using middlerApp.API.Helper;
using middlerApp.API.MapperProfiles.Formatters;
using middlerApp.Core.DataAccess.Entities.Models;
using middlerApp.SharedModels;

namespace middlerApp.API.MapperProfiles
{
    public class EndpointRulePermissionProfile : Profile
    {
        
        public EndpointRulePermissionProfile()
        {


            CreateMap<EndpointRulePermission, EndpointRulePermissionDto>()
                .EqualityComparison((entity, dto) => entity.Id == dto.Id);

            CreateMap<EndpointRulePermissionDto, EndpointRulePermission>()
                .EqualityComparison((dto, entity) => dto.Id == entity.Id);

          
        }
    }
}
