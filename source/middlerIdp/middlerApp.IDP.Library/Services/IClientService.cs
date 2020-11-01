using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using middlerApp.IDP.DataAccess.Entities.Entities;
using middlerApp.IDP.Library.DtoModels;

namespace middlerApp.IDP.Library.Services
{
    public interface IClientService
    {

        Task<List<MClientDto>> GetAllClientDtosAsync();

        Task<Client> GetClientAsync(Guid id);

        Task<MClientDto> GetClientDtoAsync(Guid id);

        Task CreateClientAsync(MClientDto clientDto);


        Task UpdateClientAsync(Client updated);
        Task DeleteClientAsync(params Guid[] id);
    }
}
