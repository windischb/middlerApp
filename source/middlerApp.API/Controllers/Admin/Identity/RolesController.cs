﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using middlerApp.API.Attributes;
using middlerApp.IDP.Library.DtoModels;
using middlerApp.IDP.Library.Services;

namespace middlerApp.API.Controllers.Admin.Identity
{
    [ApiController]
    [Route("api/idp/roles")]
    [AdminController]
    [Authorize(Policy = "Admin")]
    public class RolesController : Controller
    {
        public IRolesService RolesService { get; }
        private readonly IMapper _mapper;


        public RolesController(IRolesService rolesService, IMapper mapper)
        {
            RolesService = rolesService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<List<MRoleListDto>>> GetRolesList()
        {

            var roles = await RolesService.GetAllRoleListDtosAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MRoleDto>> GetRole(string id)
        {

            if (id == "create")
            {
                return Ok(new MRoleDto());
            }

            if (!Guid.TryParse(id, out var guid))
                return NotFound();

            var role = await RolesService.GetRoleDtoAsync(guid);

            if (role == null)
                return NotFound();

          
            return Ok(role);


        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(MRoleDto roleDto)
        {
            await RolesService.CreateRoleAsync(roleDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(MRoleDto roleDto)
        {
           
            await RolesService.UpdateRoleAsync(roleDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {

            await RolesService.DeleteRole(id);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoles([FromBody] List<Guid> ids)
        {

            await RolesService.DeleteRole(ids.ToArray());
            return NoContent();
        }


    }
}
