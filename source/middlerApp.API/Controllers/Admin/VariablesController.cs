using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using middler.Common.SharedModels.Interfaces;
using middlerApp.API.Attributes;

namespace middlerApp.API.Controllers.Admin
{
    [ApiController]
    [Route("api/variables")]
    [AdminController]
    [Authorize(Policy = "Admin")]
    public class VariablesController: Controller
    {
        public IVariablesRepository VariablesStore { get; }

        public VariablesController(IVariablesRepository variablesStore)
        {
            VariablesStore = variablesStore;
        }


        [HttpGet("folders")]
        public IActionResult GetFolders()
        {
            return Ok(VariablesStore.GetFolderTree());
        }


    }
}
