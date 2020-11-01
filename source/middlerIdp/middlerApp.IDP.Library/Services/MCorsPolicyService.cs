using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using middlerApp.IDP.DataAccess;
using middlerApp.IDP.DataAccess.SqlServer;

namespace middlerApp.IDP.Library.Services
{
    public class MCorsPolicyService: ICorsPolicyService
    {
        public IDPDbContext DbContext { get; }

        public MCorsPolicyService(IDPDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public Task<bool> IsOriginAllowedAsync(string origin)
        {
            return DbContext.Clients.AsQueryable().AnyAsync(c => c.AllowedCorsOrigins.Select(o => o.Origin).Contains(origin));
        }

        //public async Task<bool> IsOriginAllowedAsync(string origin)
        //{
        //    var allowedOrigins = await DbContext.Clients.AsQueryable().SelectMany(c => c.AllowedCorsOrigins.Select(o => o.Origin)).ToListAsync();
        //    foreach (var allowedOrigin in allowedOrigins)
        //    {
        //        var match = Wildcard.Match(origin, allowedOrigin);
        //        if (match)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
}
