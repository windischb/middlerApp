using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using middlerApp.Events;
using middlerApp.IDP.DataAccess.Entities.Entities;
using middlerApp.IDP.Library.DtoModels;

namespace middlerApp.IDP.Library.Services
{
    public interface IAuthenticationProviderService
    {
        DataEventDispatcher EventDispatcher { get; }
        Task<List<AuthenticationProviderListDto>> GetAllListDtos();
        Task<List<AuthenticationProvider>> GetAll();
        Task<AuthenticationProvider> GetSingleAsync(Guid id);
        Task Create(AuthenticationProvider authenticationProvider);
        Task Delete(params Guid[] id);
        Task Update(AuthenticationProvider authenticationProvider);
    }
}