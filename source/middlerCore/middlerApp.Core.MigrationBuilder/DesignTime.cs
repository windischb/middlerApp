using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using middlerApp.Core.DataAccess;
using middlerApp.Core.DataAccess.Postgres;
using middlerApp.Core.DataAccess.Sqlite;
using middlerApp.Core.DataAccess.SqlServer;

namespace middlerApp.Core.MigrationBuilder
{
    public class DesignTime : IDesignTimeDbContextFactory<APPDbContext>
    {
        public APPDbContext CreateDbContext(string[] args)
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

            return sp.GetRequiredService<APPDbContext>();
        }
    }
}
