using Microsoft.AspNetCore.Mvc;
using middlerApp.API.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using middlerApp.API.IDP;
using middlerApp.API.IDP.Models;
using middlerApp.API.IDP.Services;
using middlerApp.API.Models;

namespace middlerApp.API.Controllers.Admin
{
    [ApiController]
    [Route("api/first-setup")]
    [AdminController]
    [AllowAnonymous]
    public class FirstSetupController: Controller
    {

        private readonly DefaultResourcesManager _defaultResourcesManager;
        private readonly ILocalUserService _localUserService;

        public FirstSetupController(DefaultResourcesManager defaultResourcesManager, ILocalUserService localUserService)
        {
            _defaultResourcesManager = defaultResourcesManager;
            _localUserService = localUserService;
        }


        [HttpGet]
        public async Task<IActionResult> GetSetupViewModel()
        {
            var vm = new FirstSetupViewModel();
            vm.AdminUserExists = await _defaultResourcesManager.AtLeastOneAdminUserExistsAsync();

            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFirstUser([FromBody] FirstSetupModel firstSetupModel)
        {

            var exists = await _defaultResourcesManager.AtLeastOneAdminUserExistsAsync();
            if (exists)
            {
                return BadRequest("Admin User already exists!");
            }

            var adminRole = await _defaultResourcesManager.EnsureAdminRoleExists();

            var user = new MUser();
            user.UserName = firstSetupModel.Username;
            user.Roles.Add(adminRole);
            user.Active = true;

            await _localUserService.AddUserAsync(user, firstSetupModel.Password);

            await _defaultResourcesManager.EnsureAdminClientExists(firstSetupModel.RedirectUri);

            return Ok();
        }
    }

    public class FirstSetupViewModel
    {
        public bool AdminUserExists { get; set; }
    }
}
