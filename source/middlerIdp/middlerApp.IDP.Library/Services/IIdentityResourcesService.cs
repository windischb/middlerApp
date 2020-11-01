using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using middlerApp.IDP.DataAccess.Entities.Entities;
using middlerApp.IDP.Library.DtoModels;

namespace middlerApp.IDP.Library.Services
{
    public interface IIdentityResourcesService
    {

        Task<List<MScopeListDto>> GetAllIdentityResourceDtosAsync();

        Task<Scope> GetIdentityResourceAsync(Guid id);

        Task<MScopeDto> GetIdentityResourceDtoAsync(Guid id);

        Task CreateIdentityResourceAsync(MScopeDto clientDto);


        Task UpdateIdentityResourceAsync(Scope updated);
        Task DeleteIdentityResourceAsync(params Guid[] id);
    }
}
