using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper.Internal;
using JintTsDefinition;
using MailKit;
using MailKit.Net.Imap;
using McMaster.NETCore.Plugins;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using middler.Action.Scripting.Models;
using middlerApp.Agents.Shared;
using middlerApp.API.Helper;
using Reflectensions.ExtensionMethods;
using Reflectensions.HelperClasses;
using Scripter.Shared;

namespace middlerApp.API.ExtensionMethods
{
    public static class IScripterContextExtensions
    {
        
        internal static List<Assembly> assembliesForJint { get; } = new List<Assembly>();

        public static IScripterContext AddModulePlugins(this IScripterContext context)
        {
            
           
            var dir = PathHelper.GetFullPath("Scripter\\Modules");
            var sharedDllsDir = PathHelper.GetFullPath("Scripter\\Modules\\SharedDLLs");
            
            var assembliesToLoad = new List<string>();
            var tsDefinitionAssemblies = new List<string>();

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (!Directory.Exists(sharedDllsDir))
            {
                Directory.CreateDirectory(sharedDllsDir);
            }

            foreach (var file in Directory.GetFiles(sharedDllsDir, "*.dll"))
            {
                var ass = Assembly.LoadFrom(file);
                assembliesForJint.Add(ass);
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

                foreach (var file in Directory.GetFiles(directory, "*.Definition.dll"))
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
                    config =>
                    {
                        config.PreferSharedTypes = true;
                        config.LoadInMemory = true;

                    });
                loaders.Add(loader);
            }

            foreach (var loader in loaders)
            {
                using (loader.EnterContextualReflection())
                {
                    foreach (var pluginType in loader
                        .LoadDefaultAssembly()
                        .GetTypes()
                        .Where(t => typeof(IScripterModule).IsAssignableFrom(t) && !t.IsAbstract))
                    {
                        assembliesForJint.Add(pluginType.Assembly);
                        context.AddScripterModule(pluginType);
                    }
                }

                
            }

            return context;
        }
    }
}
