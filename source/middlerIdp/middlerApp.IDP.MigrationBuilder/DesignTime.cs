using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using middlerApp.IDP.DataAccess;
using middlerApp.IDP.DataAccess.Postgres;
using middlerApp.IDP.DataAccess.Sqlite;
using middlerApp.IDP.DataAccess.SqlServer;

namespace middlerApp.IDP.MigrationBuilder
{
    public class DesignTime : IDesignTimeDbContextFactory<IDPDbContext>
    {
        public IDPDbContext CreateDbContext(string[] args)
        {
            var serviceCollection = new ServiceCollection();

#if SQLITE
            SqliteServiceBuilder.AddCoreDbContext(serviceCollection, "Data Source=file.db");
#endif

#if SQLSERVER
            SqlServerServiceBuilder.AddCoreDbContext(serviceCollection, "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MiddlerApp");
#endif

#if POSTGRES
            PostgresServiceBuilder.AddCoreDbContext(serviceCollection, "Host=10.0.0.22;Database=MidlerApp;Username=postgres;Password=postgres");
#endif

            //serviceCollection.AddCoreDbContextSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MiddlerApp");

            var sp = serviceCollection.BuildServiceProvider();

            return sp.GetRequiredService<IDPDbContext>();
        }
    }
}
