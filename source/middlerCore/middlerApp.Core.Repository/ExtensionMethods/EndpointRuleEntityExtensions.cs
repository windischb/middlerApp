using System.Linq;
using middler.Common.SharedModels.Models;
using middlerApp.Core.DataAccess.Entities.Models;

namespace middlerApp.Core.Repository.ExtensionMethods
{
    public static class EndpointRuleEntityExtensions
    {
        public static MiddlerRule ToMiddlerRule(this EndpointRuleEntity entity)
        {
            if (entity == null)
                return null;

            var mRule = new MiddlerRule();
            mRule.Name = entity.Name;
            mRule.Hostname = entity.Hostname;
            mRule.Path = entity.Path;
            mRule.Scheme = MappingHelper.Split(entity.Scheme);
            mRule.HttpMethods = MappingHelper.Split(entity.HttpMethods);
            mRule.Actions = entity.Actions.Select(ToMiddlerAction).ToList();

            return mRule;
        }

        public static MiddlerAction ToMiddlerAction(this EndpointActionEntity entity)
        {
            var mAction = new MiddlerAction();
            mAction.Id = entity.Id;
            mAction.ActionType = entity.ActionType;
            mAction.Enabled = entity.Enabled;
            mAction.Terminating = entity.Terminating;
            mAction.WriteStreamDirect = entity.WriteStreamDirect;
            mAction.Parameters = entity.Parameters;
            
            return mAction;
        }


    }
}
