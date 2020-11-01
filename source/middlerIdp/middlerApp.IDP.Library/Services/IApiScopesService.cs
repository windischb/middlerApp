using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using middlerApp.IDP.DataAccess.Entities.Entities;
using middlerApp.IDP.Library.DtoModels;

namespace middlerApp.IDP.Library.Services
{
    public interface IApiScopesService
    {

        Task<List<MScopeListDto>> GetAllApiScopeDtosAsync();

        Task<Scope> GetApiScopeAsync(Guid id);

        Task<MScopeDto> GetApiScopeDtoAsync(Guid id);

        Task CreateApiScopeAsync(MScopeDto clientDto);


        Task UpdateApiScopeAsync(Scope updated);
        Task DeleteApiScopeAsync(params Guid[] id);
    }
}
