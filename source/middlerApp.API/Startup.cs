using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Jint;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using middler.Action.Scripting;
using middler.Action.Scripting.Models;
using middler.Action.UrlRedirect;
using middler.Action.UrlRewrite;
using middler.Common.SharedModels.Enums;
using middler.Core;
using middler.TaskHelper.ScripterModule;
using middler.Variables.ScripterModule;
using middlerApp.Agents.Shared;
using middlerApp.API.Attributes;
using middlerApp.API.ExtensionMethods;
using middlerApp.API.Helper;
using middlerApp.API.Middleware;
using middlerApp.API.Providers;
using middlerApp.API.TsDefinitions;
using middlerApp.Core.DataAccess;
using middlerApp.Core.Repository;
using middlerApp.Core.Repository.ExtensionMethods;
using middlerApp.GlobalVariables.ScripterModule;
using middlerApp.IDP.Library;
using NamedServices.Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Prise;
using Prise.DependencyInjection;
using Reflectensions;
using Reflectensions.ExtensionMethods;
using Reflectensions.JsonConverters;
using Scripter;
using Scripter.Engine.JavaScript;
using Scripter.Engine.PowerShellCore;
using Scripter.Engine.TypeScript;
using Scripter.Module.Http;
using Scripter.Modules.Default;
using SignalARRR.Server;
using SignalARRR.Server.ExtensionMethods;
using DecimalJsonConverter = middlerApp.API.JsonConverters.DecimalJsonConverter;
using ExpandoObjectConverter = Newtonsoft.Json.Converters.ExpandoObjectConverter;
using IScripterContextExtensions = middlerApp.API.ExtensionMethods.IScripterContextExtensions;

namespace middlerApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            
            Configuration = configuration;
            
        }

        private StartUpConfiguration NormalizeConfig()
        {
            var sConfig = Configuration.Get<StartUpConfiguration>();
            sConfig.SetDefaultSettings();
            //Static.DbProvider = sConfig.DbSettings.Provider;
            //if (sConfig.DbSettings.Provider.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
            //{
                
            //    var conBuilder = new SqliteConnectionStringBuilder(sConfig.DbSettings.ConnectionString);
            //    conBuilder.DataSource = PathHelper.GetFullPath(conBuilder.DataSource);
            //    sConfig.DbSettings.ConnectionString = conBuilder.ConnectionString;
            //    var dbdir = new DirectoryInfo(Path.GetDirectoryName(conBuilder.DataSource));
            //    if (!dbdir.Exists)
            //    {
            //        try
            //        {
            //            dbdir.Create();
            //        }
            //        catch
            //        {
            //            // ignored
            //        }
            //    }
            //}

            return sConfig;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            Json.Converter.RegisterJsonConverter<ExpandoObjectConverter>(0);

            var sConfig = this.NormalizeConfig();
            services.AddScoped<StartUpConfiguration>((provider => sConfig));

           
            services.AddMvc(options =>
            {


            }).AddNewtonsoftJson(options =>
            {

                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                //options.SerializerSettings.Converters.Add(new PSObjectJsonConverter());
                options.SerializerSettings.Converters.Add(new DecimalJsonConverter());
                options.SerializerSettings.Converters.Add(new IpAddressConverter());
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            
            services.AddResponseCompression();
            services.AddSpaStaticFiles();


           services.AddSignalR().AddNewtonsoftJsonProtocol(options =>
            {
                options.PayloadSerializerSettings.ContractResolver = new DefaultContractResolver();
                options.PayloadSerializerSettings.Converters.Add(new StringEnumConverter());
                options.PayloadSerializerSettings.Converters.Add(new ExpandoObjectConverter());
                options.PayloadSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddSignalARRR();


            services.AddAutoMapper(config =>
            {
                config.AddCollectionMappers();
            } ,Assembly.GetExecutingAssembly(), typeof(IdpConfiguration).Assembly);

            services.AddScripter(context =>
                context
                    .AddJavaScriptEngine()
                    .AddTypeScriptEngine()
                    .AddPowerShellCoreEngine()
                    .AddModulePlugins()
                    .AddScripterModule<EndpointModule>()
                    .AddScripterModule<GlobalVariablesModule>()
                    .AddScripterModule<TestModule.TestModule>()
            );

           
            services.AddScoped<Options>(provider =>
            {
                var opts = new Options();
                opts.AddExtensionMethods(typeof(StringExtensions));
                opts.CatchClrExceptions();
                opts.DebugMode();

                var JintAssemblies = new List<Assembly>();
                JintAssemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());
                JintAssemblies.AddRange(IScripterContextExtensions.assembliesForJint);

                opts.AllowClr(JintAssemblies.ToArray());
                
                return opts;
            });

            services.AddMiddler(options =>
                options
                    .AddUrlRedirectAction()
                    .AddUrlRewriteAction()
                    .AddScriptingAction()
                    .SetDefaultAccessMode(AccessMode.Ignore)

                
            );

            

            var idpConfig = new IdpConfiguration()
            {
                AdminUIPostLogoutUris = new List<string>
                {
                    IdpUriGenerator.GenerateRedirectUri(sConfig.AdminSettings.ListeningIP,
                        sConfig.AdminSettings.HttpsPort),
                    IdpUriGenerator.GenerateRedirectUri(sConfig.AdminSettings.ListeningIP, 4200)
                },
                AdminUIRedirectUris = new List<string>
                {
                    IdpUriGenerator.GenerateRedirectUri(sConfig.AdminSettings.ListeningIP,
                        sConfig.AdminSettings.HttpsPort),
                    IdpUriGenerator.GenerateRedirectUri(sConfig.AdminSettings.ListeningIP, 4200)
                }
            };

            services.AddScoped<IMiddlerAgentsService, MiddlerAgentsService>();

            services.AddMiddlerServices(sConfig.DbSettings.Provider, sConfig.DbSettings.ConnectionString);
            services.AddMiddlerIdentityServer(sConfig.DbSettings.Provider, sConfig.DbSettings.ConnectionString, idpConfig);
            //services.AddDbContext<APPDbContext>(opt => opt.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MiddlerApp"));
            //services.AddMiddlerServices("sqlserver", "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MiddlerApp");
            //services.AddMiddlerServices("postgres", "Host=10.0.0.22;Database=MiddlerApp;Username=postgres;Password=postgres");
            //services.AddMiddlerServices("sqlite", "Data Source = file.sqlite3");
            //services.AddCoreDbContextSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MiddlerApp");
            //services.AddMiddlerIdentityServer("sqlserver", "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MiddlerApp", idpConfig);
            //services.AddMiddlerIdentityServer("postgres",
            //    "Host=10.0.0.22;Database=MiddlerApp;Username=postgres;Password=postgres", idpConfig);
            //{
            //    AdminUIPostLogoutUris = new List<string>
            //    {
            //        IdpUriGenerator.GenerateRedirectUri(sConfig.AdminSettings.ListeningIP, sConfig.AdminSettings.HttpsPort),
            //        IdpUriGenerator.GenerateRedirectUri(sConfig.AdminSettings.ListeningIP, 4200)
            //    },
            //    AdminUIRedirectUris = new List<string>
            //    {
            //        IdpUriGenerator.GenerateRedirectUri(sConfig.AdminSettings.ListeningIP, sConfig.AdminSettings.HttpsPort),
            //        IdpUriGenerator.GenerateRedirectUri(sConfig.AdminSettings.ListeningIP, 4200)
            //    }
            //});

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
            
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                //options.ForwardLimit = 4;
                //options.KnownProxies.Add(IPAddress.Parse("127.0.10.1"));
                //options.ForwardedForHeaderName = "X-Forwarded-For-My-Custom-Header-Name";
                options.ForwardedHeaders =  ForwardedHeaders.All;
            });


            services.AddScoped<AuthenticationProviderContextService>();
            services.AddHostedService<AuthenticationProviderContextHostedService>();

            services.AddNamedTransient<IAuthHandler, WindowsAuthHandler>("Windows");

            services.AddSingleton<TsDefinitionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> _logger, StartUpConfiguration startUpConfiguration)
        {
            
            app.UseForwardedHeaders();
            app.UseResponseCompression();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseWhen(context => context.IsIdpAreaRequest(), ConfigureIDP);

            app.UseWhen(context => context.IsAdminAreaRequest(), ConfigureAdministration);

            app.UseWhen(context => !context.IsAdminAreaRequest() && !context.IsIdpAreaRequest(),ConfigureMiddler);

            app.Run(async context =>
            {
                await context.NotFound("Nix gefunden");
            });



        }

      
        public void ConfigureIDP(IApplicationBuilder app)
        {
            app.AddLogging();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllersWithAttribute<IdPControllerAttribute>();

            });

            app.UseMiddlerSpaUI(Static.StartUpConfiguration.IdpSettings.WebRoot);
        }

        public void ConfigureAdministration(IApplicationBuilder app)
        {
            app.AddLogging();
            
            app.UseRouting();
            
            app.UseSignalARRRAccessTokenValidation();
            app.UseAuthentication();
            app.UseAuthorization();

            
            //app.UseMiddleware<LogClaimsMiddleware>();


            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllersWithAttribute<AdminControllerAttribute>();
                endpoints.MapHub<UIHub>("/signalr/ui");
                endpoints.MapHARRRController<RemoteAgentHub>("/signalr/ra");

            });

            app.UseMiddlerSpaUI(Static.StartUpConfiguration.AdminSettings.WebRoot);
        }

        public void ConfigureMiddler(IApplicationBuilder app)
        {
            app.AddLogging();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddler(map =>
            {
                map.AddRepo<EFCoreMiddlerRepository>();
            });
        }
    }
}
