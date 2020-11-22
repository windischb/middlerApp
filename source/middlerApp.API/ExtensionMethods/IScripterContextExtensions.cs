using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using McMaster.NETCore.Plugins;
using Microsoft.Extensions.DependencyInjection;
using middlerApp.Agents.Shared;
using middlerApp.API.Helper;
using Scripter.Shared;

namespace middlerApp.API.ExtensionMethods
{
    public static class IScripterContextExtensions
    {

        public static IScripterContext AddModulePlugins(this IScripterContext context)
        {

            var dir = PathHelper.GetFullPath("Scripter\\Modules");
            var assembliesToLoad = new List<string>();

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }



            foreach (var directory in Directory.GetDirectories(dir))
            {
                foreach (var file in Directory.GetFiles(directory, "Scripter.Module.*.dll"))
                {
                    assembliesToLoad.Add(file);
                }

                foreach (var file in Directory.GetFiles(directory, "*.ScripterModule.dll"))
                {
                    assembliesToLoad.Add(file);
                }

                foreach (var file in Directory.GetFiles(directory, "*.Scripter.Module.dll"))
                {
                    assembliesToLoad.Add(file);
                }
            }

            if (!assembliesToLoad.Any())
                return context;

            var loaders = new List<PluginLoader>();

            foreach (var assembly in assembliesToLoad)
            {
               
                var loader = PluginLoader.CreateFromAssemblyFile(
                    assembly,
                    config => config.PreferSharedTypes = true);
                loaders.Add(loader);
            }

            // Create an instance of plugin types
            foreach (var loader in loaders)
            {
                using (loader.EnterContextualReflection())
                {
                    foreach (var pluginType in loader
                        .LoadDefaultAssembly()
                        .GetTypes()
                        .Where(t => typeof(IScripterModule).IsAssignableFrom(t) && !t.IsAbstract))
                    {
                        context.AddScripterModule(pluginType);
                    }
                }

                
            }

            return context;
        }
    }
}
