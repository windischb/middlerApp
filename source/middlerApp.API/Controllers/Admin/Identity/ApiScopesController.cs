﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using middlerApp.API.Attributes;
using middlerApp.IDP.DataAccess.Entities.Entities;
using middlerApp.IDP.Library.DtoModels;
using middlerApp.IDP.Library.Services;

namespace middlerApp.API.Controllers.Admin.Identity
{
    [ApiController]
    [Route("api/idp/api-scopes")]
    [AdminController]
    [Authorize(Policy = "Admin")]
    public class ApiScopesController : Controller
    {
        public IApiScopesService ApiScopesService { get; }
        private readonly IMapper _mapper;


        public ApiScopesController(IApiScopesService apiScopesService, IMapper mapper)
        {
            ApiScopesService = apiScopesService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<List<MScopeListDto>>> GetList()
        {

            var resources = await ApiScopesService.GetAllApiScopeDtosAsync();
            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MScopeDto>> Get(string id)
        {

            if (id == "create")
            {
                return Ok(_mapper.Map<MScopeDto>(new Scope()));
            }

            if (!Guid.TryParse(id, out var guid))
                return NotFound();

            var apiResource = await ApiScopesService.GetApiScopeDtoAsync(guid);

            if (apiResource == null)
                return NotFound();

          
            return Ok(apiResource);


        }

        [HttpPost]
        public async Task<IActionResult> CreateApiScope(MScopeDto dto)
        {
            await ApiScopesService.CreateApiScopeAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateApiScope(MScopeDto dto)
        {
            var resourceInDB = await ApiScopesService.GetApiScopeAsync(dto.Id);

            var updated = _mapper.Map(dto, resourceInDB);

            await ApiScopesService.UpdateApiScopeAsync(updated);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApiScope(Guid id)
        {

            await ApiScopesService.DeleteApiScopeAsync(id);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteApiScopes([FromBody] List<Guid> ids)
        {

            await ApiScopesService.DeleteApiScopeAsync(ids.ToArray());
            return NoContent();
        }


    }
}
