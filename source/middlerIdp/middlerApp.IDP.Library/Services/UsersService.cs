using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using middlerApp.Events;
using middlerApp.IDP.DataAccess;
using middlerApp.IDP.DataAccess.Entities.Models;
using middlerApp.IDP.DataAccess.SqlServer;
using middlerApp.IDP.Library.DtoModels;

namespace middlerApp.IDP.Library.Services
{
    public class UsersService : IUsersService
    {
        private readonly IMapper _mapper;
        private IDPDbContext DbContext { get; }
        public DataEventDispatcher EventDispatcher { get; }


        public UsersService(IDPDbContext dbContext, IMapper mapper, DataEventDispatcher eventDispatcher)
        {
            _mapper = mapper;
            DbContext = dbContext;
            EventDispatcher = eventDispatcher;
        }


        public async Task<List<MUserListDto>> GetAllUserListDtosAsync()
        {
            var users = await DbContext.Users.AsNoTracking().ToListAsync();
            return _mapper.Map<List<MUserListDto>>(users);
        }

        public async Task<MUser> GetUserAsync(Guid id)
        {
            var query = DbContext.Users
                .Include(u => u.Claims)
                .Include(u => u.Logins)
                .Include(u => u.Secrets)
                .Include(u => u.ExternalClaims)
                .Include(u => u.Roles).AsQueryable();

            return await query.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<MUserDto> GetUserDtoAsync(Guid id)
        {
            var user = await GetUserAsync(id);
            return _mapper.Map<MUserDto>(user);
        }

        public async Task UpdateUserAsync(MUser userModel)
        {
            //var att = _context.Users.Attach(userModel);
            //att.State = EntityState.Modified;

            //var roleIds = userModel.Roles.Select(ur => ur.Id).ToList();
            //var availableRoles = DbContext.Roles.AsNoTracking().Where(r => roleIds.Contains(r.Id)).Select(r => r.Id).ToList();

            //userModel.Roles = userModel.Roles.Where(ur => availableRoles.Contains(ur.Id)).ToList();

            await DbContext.SaveChangesAsync();
            EventDispatcher.DispatchUpdatedEvent("IDPUsers", _mapper.Map<MUserDto>(userModel));
        }

        public async Task UpdateUserAsync(MUserDto userDto)
        {

            var userInDB = await GetUserAsync(userDto.Id);
            _mapper.Map(userDto, userInDB);
            var roleIds = userDto.Roles.Select(ur => ur.Id).ToList();
            var availableRoles = DbContext.Roles.Where(r => roleIds.Contains(r.Id)).ToList();

            userInDB.Roles = availableRoles;

            await DbContext.SaveChangesAsync();
            EventDispatcher.DispatchUpdatedEvent("IDPUsers", _mapper.Map<MUserDto>(userInDB));
        }

        public async Task DeleteUser(params Guid[] id)
        {
            var users = await DbContext.Users.Where(u => id.Contains(u.Id)).ToListAsync();
            DbContext.Users.RemoveRange(users);
            await DbContext.SaveChangesAsync();
            EventDispatcher.DispatchDeletedEvent("IDPUsers", users.Select(r => r.Id));
        }


    }
}
