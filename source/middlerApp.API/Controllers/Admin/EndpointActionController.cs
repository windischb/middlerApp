using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using middler.Action.Scripting;
using middler.Common.SharedModels.Models;
using middler.Core;
using middlerApp.API.Attributes;
using middlerApp.Core.DataAccess.Entities.Models;
using middlerApp.Core.Repository;
using middlerApp.SharedModels;

namespace middlerApp.API.Controllers.Admin
{
    [ApiController]
    [Route("api/endpoint-action")]
    [AdminController]
    public class EndpointActionController: Controller
    {
        private readonly IMapper _mapper;
        private readonly InternalHelper _internalHelper;
        private readonly EndpointRuleRepository _endpointRuleRepository;

        public EndpointActionController(IMapper mapper, InternalHelper internalHelper, EndpointRuleRepository endpointRuleRepository)
        {
            _mapper = mapper;
            _internalHelper = internalHelper;
            _endpointRuleRepository = endpointRuleRepository;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAction(Guid id, EndpointActionDto actionDto)
        {
            var actn = await _endpointRuleRepository.FindAction(actionDto.Id.Value);
            _mapper.Map(actionDto, actn);
            
            UpdateAction(actn);
            await _endpointRuleRepository.UpdateActionAsync(actn);
            //await _endpointRuleRepository.AddActionToRule(entity);

            return Ok();
        }

        private void UpdateAction(EndpointActionEntity actionEntity)
        {
            var mAction = _mapper.Map<MiddlerAction>(actionEntity);
            var action = _internalHelper.BuildConcreteActionInstance(mAction);
            if (action == null)
            {
                return;
            }

            if (action is ScriptingAction scriptAction)
            {
                actionEntity.Parameters["CompiledCode"] = scriptAction?.CompileScriptIfNeeded();
            }

        }
    }
}
