using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using middlerApp.IDP.DataAccess.Entities.Models;
using middlerApp.IDP.Library.DtoModels;

namespace middlerApp.IDP.Library.Services
{
    public interface IUsersService
    {
        Task<List<MUserListDto>> GetAllUserListDtosAsync();

        Task<MUser> GetUserAsync(Guid id);

        Task<MUserDto> GetUserDtoAsync(Guid id);


        Task DeleteUser(params Guid[] ids);
        Task UpdateUserAsync(MUser updated);
        Task UpdateUserAsync(MUserDto userDto);
    }
}
