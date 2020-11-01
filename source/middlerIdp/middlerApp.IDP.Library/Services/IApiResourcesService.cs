using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using middlerApp.IDP.DataAccess.Entities.Entities;
using middlerApp.IDP.Library.DtoModels;

namespace middlerApp.IDP.Library.Services
{
    public interface IApiResourcesService
    {

        Task<List<MApiResourceListDto>> GetAllApiResourceDtosAsync();

        Task<ApiResource> GetApiResourceAsync(Guid id);

        Task<MApiResourceDto> GetApiResourceDtoAsync(Guid id);

        Task CreateApiResourceAsync(MApiResourceDto clientDto);


        Task UpdateApiResourceAsync(ApiResource updated);
        Task DeleteApiResourceAsync(params Guid[] id);
    }
}
