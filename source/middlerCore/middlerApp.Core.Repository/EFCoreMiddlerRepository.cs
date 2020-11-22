using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using middler.Common.Interfaces;
using middler.Common.SharedModels.Models;
using middlerApp.Core.DataAccess;
using middlerApp.Core.Repository.ExtensionMethods;

namespace middlerApp.Core.Repository
{
    public class EFCoreMiddlerRepository : IMiddlerRepository
    {
        public APPDbContext AppDbContext { get; }

        public EFCoreMiddlerRepository(APPDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public List<MiddlerRule> ProvideRules()
        {
            var rules = AppDbContext
                .EndpointRules.AsQueryable()
                .Where(er => er.Enabled)
                .Include(r => r.Actions)
                .Include(r => r.Permissions)
                .ToList()
                .Select(r => r.ToMiddlerRule())
                .ToList();

            return rules;

        }
    }
}
