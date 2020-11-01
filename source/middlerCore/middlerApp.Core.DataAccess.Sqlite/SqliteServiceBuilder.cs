using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace middlerApp.Core.DataAccess.Sqlite
{
    public static class SqliteServiceBuilder
    {
        public static void AddCoreDbContext(IServiceCollection serviceCollection, string connectionString)
        {

            serviceCollection.AddDbContext<APPDbContext>(opt => opt.UseSqlite(connectionString, sql => sql.MigrationsAssembly(typeof(SqliteServiceBuilder).Assembly.FullName)));
        }
    }
}
