using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using middler.Common.SharedModels.Attributes;
using middler.Core;
using middlerApp.API.Helper;
using middlerApp.Core.DataAccess.Entities.Models;
using middlerApp.SharedModels;
using Reflectensions;

namespace middlerApp.API.ExtensionMethods
{
    public static class EndpointRuleEntityExtensions
    {
        public static EndpointRuleDto ToEndpointRuleDto(this EndpointRuleEntity entity, InternalHelper internalHelper)
        {
            if (entity == null)
                return null;

            var dto = new EndpointRuleDto();
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.Enabled = entity.Enabled;
            dto.Hostname = entity.Hostname;
            dto.Path = entity.Path;
            dto.Order = entity.Order;
            dto.Scheme = MappingHelper.Split(entity.Scheme);
            dto.HttpMethods = MappingHelper.Split(entity.HttpMethods);
            dto.Actions = entity.Actions.Select(a => a.ToEndpointRuleDto(internalHelper)).ToList();

            return dto;
        }

        public static EndpointActionDto ToEndpointRuleDto(this EndpointActionEntity entity, InternalHelper internalHelper)
        {

            var actionType = internalHelper.GetConcreteActionType(entity.ActionType);
            var optionsType = actionType.BaseType.GetGenericArguments().FirstOrDefault();

            var dto = new EndpointActionDto();
            dto.Id = entity.Id;
            dto.ActionType = entity.ActionType;
            dto.Enabled = entity.Enabled;
            dto.EndpointRuleEntityId = entity.EndpointRuleEntityId;
            dto.Order = entity.Order;
            dto.Terminating = entity.Terminating;
            dto.WriteStreamDirect = entity.WriteStreamDirect;
            var tempDict = Json.Converter.ToDictionary(entity.Parameters);
            var nDict = new Dictionary<string, object>();
            foreach (var (key, value) in tempDict)
            {
                var prop = optionsType.GetProperty(key);
                if(prop == null)
                    continue;

                var internalProp = prop.GetCustomAttribute<InternalAttribute>();
                if (internalProp == null)
                {
                    nDict.Add(key, value);
                }
            }

            dto.Parameters = nDict;
            

            return dto;
        }
    }
}
