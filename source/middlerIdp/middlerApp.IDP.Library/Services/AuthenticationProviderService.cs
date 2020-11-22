using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using middlerApp.Events;
using middlerApp.IDP.DataAccess;
using middlerApp.IDP.DataAccess.Entities.Entities;
using middlerApp.IDP.Library.DtoModels;
using middlerApp.IDP.Library.ExtensionMethods;

namespace middlerApp.IDP.Library.Services
{
    public class AuthenticationProviderService : IAuthenticationProviderService
    {
        private readonly IMapper _mapper;
        private IDPDbContext DbContext { get; }

        public DataEventDispatcher EventDispatcher { get; }

        public AuthenticationProviderService(IDPDbContext dbContext, IMapper mapper, DataEventDispatcher eventDispatcher)
        {
            _mapper = mapper;
            DbContext = dbContext;
            EventDispatcher = eventDispatcher;
        }

        public async Task<List<AuthenticationProviderListDto>> GetAllListDtos()
        {
            var providers = await DbContext.AuthenticationProviders.ToListAsync();
            return providers.Select(p => p.ToListDto()).ToList();
        }

        public Task<List<AuthenticationProvider>> GetAll()
        {
            return DbContext.AuthenticationProviders.ToListAsync();
        }

        public Task<AuthenticationProvider> GetSingleAsync(Guid id)
        {
            return DbContext.AuthenticationProviders.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Create(AuthenticationProvider authenticationProvider)
        {
            await DbContext.AuthenticationProviders.AddAsync(authenticationProvider);
            await DbContext.SaveChangesAsync();

            EventDispatcher.DispatchCreatedEvent("AuthenticationProvider", authenticationProvider);
        }

        public async Task Delete(params Guid[] id)
        {
            var providers = await DbContext.AuthenticationProviders.Where(p => id.Contains(p.Id)).ToListAsync();
            DbContext.AuthenticationProviders.RemoveRange(providers);
            await DbContext.SaveChangesAsync();
            EventDispatcher.DispatchDeletedEvent("AuthenticationProvider", providers.Select(r => r.Id));
        }

        public async Task Update(AuthenticationProvider authenticationProvider)
        {
            await DbContext.SaveChangesAsync();
            EventDispatcher.DispatchUpdatedEvent("AuthenticationProvider", authenticationProvider);
        }
    }
}
