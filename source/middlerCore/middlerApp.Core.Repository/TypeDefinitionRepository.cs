using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using middlerApp.Core.DataAccess;
using middlerApp.Core.DataAccess.Entities.Models;
using middlerApp.Events;

namespace middlerApp.Core.Repository
{
    public class TypeDefinitionRepository
    {
        public DataEventDispatcher EventDispatcher { get; }
        private readonly APPDbContext _appDbContext;

        public TypeDefinitionRepository(APPDbContext appDbContext, DataEventDispatcher eventDispatcher)
        {
            EventDispatcher = eventDispatcher;
            _appDbContext = appDbContext;
        }

        public Task<List<TypeDefinition>> GetAll()
        {
            return _appDbContext.TypeDefinitions.ToListAsync();
        }

        public async Task Add(TypeDefinition td)
        {
            await _appDbContext.TypeDefinitions.AddAsync(td);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            var found =await  _appDbContext.TypeDefinitions.FirstOrDefaultAsync(t => t.Id == id);
            if (found != null)
            {
                _appDbContext.TypeDefinitions.Remove(found);
            }
        }
    }
}
