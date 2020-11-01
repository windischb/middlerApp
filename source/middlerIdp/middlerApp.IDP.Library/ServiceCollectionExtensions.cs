using System;
using IdentityServer4;
using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using middlerApp.Events;
using middlerApp.IDP.DataAccess.Entities.Models;
using middlerApp.IDP.DataAccess.Postgres;
using middlerApp.IDP.DataAccess.Sqlite;
using middlerApp.IDP.DataAccess.SqlServer;
using middlerApp.IDP.Library.Services;
using middlerApp.IDP.Library.Storage.Stores;

namespace middlerApp.IDP.Library
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMiddlerIdentityServer(this IServiceCollection services, string provider, string connectionstring, IdpConfiguration idpConfiguration = null)
        {


            idpConfiguration ??= new IdpConfiguration();
            services.AddSingleton(idpConfiguration);

            var _provider = provider?.ToLower();
            switch (_provider)
            {
                case "sqlite":
                    {
                        SqliteServiceBuilder.AddCoreDbContext(services, connectionstring);
                        break;
                    }
                case "sqlserver":
                    {
                        SqlServerServiceBuilder.AddCoreDbContext(services, connectionstring);
                        break;
                    }
                case "postgres":
                {
                    PostgresServiceBuilder.AddCoreDbContext(services, connectionstring);
                    break;
                }
                default:
                {
                        throw new NotSupportedException($"Database Provider '{provider}' is not supported!");
                }
            }

            services.AddSingleton<DataEventDispatcher>();

            services.AddScoped<IPasswordHasher<MUser>, PasswordHasher<MUser>>();
            services.AddScoped<ILocalUserService, LocalUserService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IApiResourcesService, ApiResourcesService>();
            services.AddScoped<IIdentityResourcesService, IdentityResourcesService>();
            services.AddScoped<IApiScopesService, ApiScopesService>();



            //services.AddScoped<IAuthorizationCodeStore, AuthorizationCodeStore>();
            services.AddScoped<IUserConsentStore, UserConsentStore>();

            services.AddIdentityServer(options =>
                {
                    options.UserInteraction.LoginUrl = "/login";
                    options.UserInteraction.ConsentUrl = "/consent";
                    options.UserInteraction.LogoutUrl = "/logout";
                    options.UserInteraction.ErrorUrl = "/error";
                })
                .AddDeveloperSigningCredential()
                .AddCorsPolicyService<MCorsPolicyService>()
                .AddClientStore<ClientStore>()
                .AddDeviceFlowStore<DeviceFlowStore>()
                .AddResourceStore<ResourceStore>()
                .AddPersistedGrantStore<PersistedGrantStore>()
                //.AddClientConfigurationValidator<CustomClientConfigurationValidator>()
                .AddProfileService<LocalUserProfileService>()
                .AddResourceValidator<MResourceValidator>()
                .AddAuthorizeInteractionResponseGenerator<MAuthorizeInteractionResponseGenerator>();


            services.AddTransient<ICorsPolicyService, MCorsPolicyService>();
            services.AddTransient<IUserInfoResponseGenerator, MUserInfoResponseGenerator>();
            services.AddTransient<ICustomTokenValidator, MCustomTokenValidator>();
            services.AddAuthentication().AddLocalApi(options =>
            {
                options.ExpectedScope = "IdentityServerApi";
            });

            //services.AddAuthentication().AddLocalAccessTokenValidation();


            //services.AddAuthentication().AddJwtBearer("Bearer", options =>
            //{
            //    var handler = new SocketsHttpHandler();
            //    handler.SslOptions.RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            //    options.BackchannelHttpHandler = handler;

            //    options.Authority = "https://localhost:4445";
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateAudience = false
            //    };
            //});

            //services.AddAuthentication().AddOAuth2Introspection("IdentityServerAccessToken", options =>
            //{

            //    options.Authority = "https://localhost:4445";

            //    options.ClientId = "IdentityServerApi";
            //    options.ClientSecret = "test";

            //});


            services.AddAuthorization((options) =>
            {
                options.AddPolicy(IdentityServerConstants.LocalApi.PolicyName, policy =>
                {
                    policy.AddAuthenticationSchemes(IdentityServerConstants.LocalApi.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("IdentityServerAdmin");
                });
            });


            services.AddScoped<DefaultResourcesManager>();

            services.AddHostedService<EnsureDefaultResourcesExistsService>();

        }
    }
}
