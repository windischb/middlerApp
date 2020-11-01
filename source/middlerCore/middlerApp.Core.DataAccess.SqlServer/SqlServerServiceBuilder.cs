using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace middlerApp.Core.DataAccess.SqlServer
{
    public static class SqlServerServiceBuilder
    {
        public static void AddCoreDbContext(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<APPDbContext>(opt => opt.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(typeof(SqlServerServiceBuilder).Assembly.FullName)));
        }
    }
    
}
