using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using middler.Action.Scripting;
using middler.Common.SharedModels.Models;
using middler.Core;
using middlerApp.API.Attributes;
using middlerApp.API.DataAccess;
using middlerApp.API.DataAccess.ExtensionMethods;
using middlerApp.SharedModels;

namespace middlerApp.API.Controllers.Admin
{
    [ApiController]
    [Route("api/endpoint-rules")]
    [AdminController]
    public class EndpointRulesController: Controller
    {
        private readonly IMapper _mapper;
        private readonly InternalHelper _internalHelper;
        private readonly EndpointRuleRepository _endpointRuleRepository;

        public EndpointRulesController(IServiceProvider serviceProvider, IMapper mapper, InternalHelper internalHelper, EndpointRuleRepository endpointRuleRepository)
        {
            _mapper = mapper;
            _internalHelper = internalHelper;
            _endpointRuleRepository = endpointRuleRepository;

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<EndpointRuleListDto>>> GetAll(bool actionDetails = false)
        {
            var rules = await _endpointRuleRepository.GetAllAsync();
            if (actionDetails)
            {
                return Ok(rules.Select(r => r.ToEndpointRuleDto(_internalHelper)));
            }
            return Ok(_mapper.Map<List<EndpointRuleListDto>>(rules));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EndpointRuleDto>> Get(Guid id)
        {
            var rule = await _endpointRuleRepository.Find(id);
            return Ok(rule.ToEndpointRuleDto(_internalHelper));
        }


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] EndpointRuleDto rule)
        {


            var entity = _mapper.Map<EndpointRuleEntity>(rule);
            foreach (var endpointActionEntity in entity.Actions)
            {
                UpdateAction(endpointActionEntity);
            }
            await _endpointRuleRepository.AddAsync(entity);

            //var dbModel = _mapper.Map<MiddlerRuleDbModel>(rule);
            //UpdateActions(dbModel);
            //await Repo.AddAsync(dbModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _endpointRuleRepository.RemoveAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EndpointRuleDto>> Update(Guid id, [FromBody] EndpointRuleDto ruleDto)
        {
            var ruleInDb = await _endpointRuleRepository.Find(id);
            var entity = _mapper.Map(ruleDto, ruleInDb);
            foreach (var endpointActionEntity in entity.Actions)
            {
                endpointActionEntity.EndpointRuleEntityId = id;
                UpdateAction(endpointActionEntity);
            }
            
            
            await _endpointRuleRepository.UpdateAsync(entity);

            var updated = await _endpointRuleRepository.GetByIdAsync(id);
            return Ok(updated.ToEndpointRuleDto(_internalHelper));
        }

        [HttpPut("{id}/enabled/{enabled:bool}")]
        public async Task<ActionResult<EndpointRuleDto>> Enabled(Guid id, bool enabled)
        {
            var ruleInDb = await _endpointRuleRepository.Find(id);
            ruleInDb.Enabled = enabled;
           
            await _endpointRuleRepository.UpdateAsync(ruleInDb);

            var updated = await _endpointRuleRepository.GetByIdAsync(id);
            return Ok(updated.ToEndpointRuleDto(_internalHelper));
        }

        //[HttpPatch("{id}")]
        //public async Task<ActionResult<EndpointRuleEntity>> PartialUpdate(Guid id, [FromBody]JsonPatchDocument<EndpointRuleDto> patchDocument) {

        //    var ruleInDb = await _endpointRuleRepository.Find(id);

        //    var updDto = _mapper.Map<EndpointRuleDto>(ruleInDb);

        //    patchDocument.ApplyTo(updDto, ModelState);

        //    _mapper.Map(updDto, ruleInDb);
        //    //UpdateActions(ruleInDb);
        //    await _endpointRuleRepository.UpdateAsync(ruleInDb);
        //    var updated = await _endpointRuleRepository.GetByIdAsync(id);
        //    return Ok(updated);
        //}

        [HttpPost("order")]
        public async Task<IActionResult> UpdateRulesOrder([FromBody] Dictionary<Guid, decimal> order)
        {

            await _endpointRuleRepository.UpdateRulesOrder(order);
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
