﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using middlerApp.API.Attributes;
using middlerApp.API.Controllers.Admin.Identity.ViewModels;
using middlerApp.IDP.DataAccess.Entities.Models;
using middlerApp.IDP.Library.DtoModels;
using middlerApp.IDP.Library.Services;

namespace middlerApp.API.Controllers.Admin.Identity
{
    [ApiController]
    [Route("api/idp/users")]
    [AdminController]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        public IUsersService UsersService { get; }
        private readonly IMapper _mapper;
        private readonly ILocalUserService _localUserService;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IRolesService _rolesService;


        public UsersController(ILocalUserService localUserService,
            IIdentityServerInteractionService interaction,
            IUsersService usersService,
            IRolesService rolesService,
            IMapper mapper)
        {
            UsersService = usersService;
            _mapper = mapper;
            _localUserService = localUserService;
            _interaction = interaction;
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MUserListDto>>> GetAllUsers()
        {

            var users = await UsersService.GetAllUserListDtosAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MUserDto>> GetUser(string id)
        {

            if (id == "create")
            {
                return Ok(new MUserDto());
            }

            if (!Guid.TryParse(id, out var guid))
                return NotFound();

            var user = await UsersService.GetUserDtoAsync(guid);

            
            if (user == null)
                return NotFound();


            return Ok(user);


        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(MUserDto createUserDto)
        {

            var userModel = _mapper.Map<MUser>(createUserDto);
            userModel.Subject = Guid.NewGuid().ToString();

            await _localUserService.AddUserAsync(userModel);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(MUserDto updateUserDto)
        {
            await UsersService.UpdateUserAsync(updateUserDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {

            await UsersService.DeleteUser(id);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsers([FromBody] List<Guid> ids)
        {

            await UsersService.DeleteUser(ids.ToArray());
            return NoContent();
        }


        [HttpPost("{id}/password")]
        public async Task<IActionResult> SetPassword(Guid id, SetPasswordDto passwordDto)
        {
            await _localUserService.SetPassword(id, passwordDto.Password);
            return Ok();
        }

        [HttpDelete("{id}/password")]
        public async Task<IActionResult> ClearPassword(Guid id)
        {
            await _localUserService.ClearPassword(id);
            return Ok();
        }
    }
}
