using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using middler.Action.Scripting;
using middler.Action.UrlRewrite;
using middler.Common.Actions.UrlRedirect;
using middler.Common.SharedModels.Interfaces;
using middler.Core;
using middlerApp.API.Attributes;
using middlerApp.API.ExtensionMethods;
using middlerApp.API.Helper;
using middlerApp.API.JsonConverters;
using middlerApp.API.Middleware;
using middlerApp.Core.DataAccess.Sqlite;
using middlerApp.Core.Repository;
using middlerApp.Core.Repository.ExtensionMethods;
using middlerApp.IDP.Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SignalARRR.Server;
using SignalARRR.Server.ExtensionMethods;



namespace middlerApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            var sConfig = Configuration.Get<StartUpConfiguration>();
            sConfig.SetDefaultSettings();

           
            services.AddMvc(options =>
            {


            }).AddNewtonsoftJson(options =>
            {

                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                //options.SerializerSettings.Converters.Add(new PSObjectJsonConverter());
                options.SerializerSettings.Converters.Add(new DecimalJsonConverter());
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            
            services.AddResponseCompression();
            services.AddSpaStaticFiles();



            services.AddSignalR().AddNewtonsoftJsonProtocol(options =>
            {
                options.PayloadSerializerSettings.ContractResolver = new DefaultContractResolver();
                options.PayloadSerializerSettings.Converters.Add(new StringEnumConverter());
                options.PayloadSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddSignalARRR();


            services.AddAutoMapper(Assembly.GetExecutingAssembly(), typeof(IdpConfiguration).Assembly);

            services.AddMiddler(options =>
                options
                    .AddUrlRedirectAction()
                    .AddUrlRewriteAction()
                    .AddScriptingAction()
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


           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> _logger)
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
                await context.Response.WriteAsync("Hello World!");
            });



        }

      
        public void ConfigureIDP(IApplicationBuilder app)
        {
            app.AddLogging();

            app.UseRouting();

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

            
            app.UseMiddleware<LogClaimsMiddleware>();


            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllersWithAttribute<AdminControllerAttribute>();
                endpoints.MapHub<UIHub>("/signalr/ui");
                endpoints.MapHub<RemoteAgentHub>("/signalr/ra");

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
