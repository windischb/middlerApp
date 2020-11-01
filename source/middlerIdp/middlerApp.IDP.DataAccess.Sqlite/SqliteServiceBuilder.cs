using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace middlerApp.IDP.DataAccess.Sqlite
{
    public static class SqliteServiceBuilder
    {
        public static void AddCoreDbContext(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<IDPDbContext>(opt => opt.UseSqlite(connectionString, sql => sql.MigrationsAssembly(typeof(SqliteServiceBuilder).Assembly.FullName)));
        }
    }
    
}
