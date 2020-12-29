using System;
using System.Net.Http;
using System.Net.Security;
using IdentityModel.AspNetCore.AccessTokenValidation;
using IdentityModel.AspNetCore.OAuth2Introspection;
using IdentityServer4;
using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using middlerApp.Events;
using middlerApp.IDP.DataAccess.Entities.Models;
using middlerApp.IDP.DataAccess.Postgres;
using middlerApp.IDP.DataAccess.Sqlite;
using middlerApp.IDP.DataAccess.SqlServer;
using middlerApp.IDP.Library.LocalTokenAuthenticatonHandler;
using middlerApp.IDP.Library.Services;
using middlerApp.IDP.Library.Storage.Stores;
using middlerApp.IDP.Library.Validators;

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
            services.AddScoped<IAuthenticationProviderService, AuthenticationProviderService>();


            //services.AddScoped<IAuthorizationCodeStore, AuthorizationCodeStore>();
            services.AddScoped<IUserConsentStore, UserConsentStore>();

            services.AddIdentityServer(options =>
                {
                    options.UserInteraction.LoginUrl = "/login";
                    options.UserInteraction.ConsentUrl = "/consent";
                    options.UserInteraction.LogoutUrl = "/logout";
                    options.UserInteraction.ErrorUrl = "/error";

                    options.Authentication.CookieAuthenticationScheme = "ids";
                    
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
                .AddAuthorizeInteractionResponseGenerator<MAuthorizeInteractionResponseGenerator>()
                .AddSecretValidator<CustomHashedSharedSecretValidator>()
                ;


            services.AddTransient<ICorsPolicyService, MCorsPolicyService>();
            services.AddTransient<IUserInfoResponseGenerator, MUserInfoResponseGenerator>();
            services.AddTransient<ICustomTokenValidator, MCustomTokenValidator>();
            services.AddTransient<IApiSecretValidator, CustomApiSecretValidator>();
            services.AddTransient<ISecretsListValidator, CustomSecretValidator>();

            services.AddHttpClient(OAuth2IntrospectionDefaults.BackChannelHttpClientName)
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new SocketsHttpHandler
                    {
                        SslOptions = new SslClientAuthenticationOptions
                        {
                            RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                        }
                    };
                });

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Bearer";
                    options.DefaultChallengeScheme = "Bearer";
                    options.DefaultSignInScheme = "ids";
                    options.DefaultSignOutScheme = "ids";

                })
                .AddCookie("ids")
                .AddJwtBearer(o =>
                {
                    o.Authority = "https://localhost:4445";
                    o.RequireHttpsMetadata = false;
                    o.ForwardDefaultSelector = Selector.ForwardReferenceToken("introspection");
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };

                })
                .AddOAuth2Introspection("introspection", options =>
                {
                    options.Authority = "https://localhost:4445";

                    options.ClientId = "api";
                    options.ClientSecret = "ABC12abc!";
                    options.EnableCaching = true;
                    options.CacheDuration = TimeSpan.FromMinutes(10);
                    options.Events = new OAuth2IntrospectionEvents();
                    options.Events.OnAuthenticationFailed = async context =>
                    {
                        context.HttpContext.Response.Headers["Warning"] = context.Error;
                    };
                    

                });
                //.AddNegotiate("Windows", "Windows Authentication", options =>
                //{
                //    options.PersistKerberosCredentials = false;
                    
                //});

            services.AddAuthorization((options) =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Administrators");
                });
            });


            services.AddScoped<DefaultResourcesManager>();

            services.AddHostedService<EnsureDefaultResourcesExistsService>();

        }


    }
}
