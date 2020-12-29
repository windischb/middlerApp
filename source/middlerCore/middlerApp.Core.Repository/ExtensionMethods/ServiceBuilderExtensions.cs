using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using middler.Common.SharedModels.Interfaces;
using middler.Core;
using middlerApp.Core.DataAccess;
using middlerApp.Core.DataAccess.Postgres;
using middlerApp.Core.DataAccess.Sqlite;
using middlerApp.Core.DataAccess.SqlServer;
using middlerApp.SharedModels.Interfaces;

namespace middlerApp.Core.Repository.ExtensionMethods
{
    public static class ServiceBuilderExtensions
    {
        public static IServiceCollection AddMiddlerServices(this IServiceCollection serviceCollection, string provider, string connectionString)
        {

            serviceCollection.AddScoped<EndpointRuleRepository>();



            serviceCollection.AddScoped<IVariablesRepository, VariablesRepository>();

            //idpConfiguration ??= new IdpConfiguration();
            //services.AddSingleton(idpConfiguration);

            var _provider = provider?.ToLower();
            switch (_provider)
            {
                case "sqlite":
                    {
                        SqliteServiceBuilder.AddCoreDbContext(serviceCollection, connectionString);
                        break;
                    }
                case "sqlserver":
                    {
                        SqlServerServiceBuilder.AddCoreDbContext(serviceCollection, connectionString);
                        break;
                    }
                case "postgres":
                    {
                        PostgresServiceBuilder.AddCoreDbContext(serviceCollection, connectionString);
                        break;
                    }
                default:
                    {
                        throw new NotSupportedException($"Database Provider '{provider}' is not supported!");
                    }
            }

            serviceCollection.AddMiddlerRepo<EFCoreMiddlerRepository>(ServiceLifetime.Scoped);

            return serviceCollection;
        }
    }
}
