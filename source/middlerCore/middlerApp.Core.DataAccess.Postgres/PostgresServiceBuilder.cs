using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace middlerApp.Core.DataAccess.Postgres
{
    public static class PostgresServiceBuilder
    {
        public static void AddCoreDbContext(IServiceCollection serviceCollection, string connectionString)
        {

            serviceCollection.AddDbContext<APPDbContext>(opt => opt.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(typeof(PostgresServiceBuilder).Assembly.FullName)));
        }
    }
}
