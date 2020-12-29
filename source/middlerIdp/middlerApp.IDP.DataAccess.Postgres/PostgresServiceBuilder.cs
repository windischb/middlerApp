using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace middlerApp.IDP.DataAccess.Postgres
{
    public static class PostgresServiceBuilder
    {
        public static void AddCoreDbContext(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<IDPDbContext>(opt =>
            {
                opt.UseNpgsql(connectionString,
                        sql => sql.MigrationsAssembly(typeof(PostgresServiceBuilder).Assembly.FullName));
                
                opt.ConfigureWarnings(w => w.Ignore(RelationalEventId.MultipleCollectionIncludeWarning));
            });
        }
    }
    
}
