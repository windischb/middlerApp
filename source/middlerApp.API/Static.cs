﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using middlerApp.API.Helper;
using Reflectensions.ExtensionMethods;
using Serilog;

namespace middlerApp.API
{
    public static class Static
    {
        public static bool RunningInDocker => 
            Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") != null && 
            Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER").ToBoolean();

        public static string DbProvider
        {
            get
            {
                return Environment.GetEnvironmentVariable("middlerAPP_DbProvider");
            }
            set
            {
                Environment.SetEnvironmentVariable("middlerAPP_DbProvider", value);
            }
        }

        public static bool IsDevelopment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development;

        public static string DomainName =>
            System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;

        public static StartUpConfiguration StartUpConfiguration { get; } = BuildConfig();

        private static StartUpConfiguration BuildConfig()
        {
            var configFilePath = PathHelper.GetFullPath("./data/configuration.json");
            if (!Static.RunningInDocker)
            {
                Log.Debug("Build Configuration");
                if (!File.Exists(configFilePath))
                {
                    Log.Debug($"New Configuration written to: {configFilePath}");
                    File.WriteAllText(configFilePath ,Converter.Json.ToJson(new StartUpConfiguration().SetDefaultSettings(), true));
                }
            }
            

            var config = new ConfigurationBuilder();
            config.AddJsonFile(configFilePath, optional: true);
            config.AddEnvironmentVariables();

            return config.Build().Get<StartUpConfiguration>();
            
        }


    }
}
