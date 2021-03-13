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
using Serilog;
using Serilog.Core;

namespace middlerApp.API.ExtensionMethods
{
    public static class IScripterContextExtensions
    {

        internal static List<Assembly> assembliesForJint { get; } = new List<Assembly>();
        internal static List<string> assemblyLocations { get; } = new List<string>();

        public static IScripterContext AddModulePlugins(this IScripterContext context)
        {


            var dir = PathHelper.GetFullPath("Scripter/Modules");
            var sharedDllsDir = PathHelper.GetFullPath("Scripter/Modules/SharedDLLs");
            

            Log.Information($"Scriper Modules Path: {dir}");
            Log.Information($"Scriper SharedDlls Path: {sharedDllsDir}");

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

                foreach (var file in Directory.GetFiles(directory, "*.dll"))
                {
                    assemblyLocations.Add(file);
                    //assembliesForJint.Add(ass);
                }

                foreach (var file in Directory.GetFiles(directory, "*.SharedModels.dll"))
                {
                    var ass = Assembly.LoadFrom(file);
                    //assembliesForJint.Add(ass);
                }

                foreach (var file in Directory.GetFiles(directory, "*.Shared.Interfaces.dll"))
                {
                    //var ass = Assembly.LoadFrom(file);
                    //assembliesToLoad.Add(file);
                    //foreach (var referencedAssembly in ass.GetReferencedAssemblies())
                    //{
                    //    if (referencedAssembly.Name.Contains("scsm", StringComparison.CurrentCultureIgnoreCase))
                    //    {
                    //        //Assembly.LoadFrom(referencedAssembly.Name + ".dll");
                    //    }

                    //}

                    //assembliesForJint.Add(ass);
                }

            }

            if (!assembliesToLoad.Any())
                return context;

            var loaders = new List<PluginLoader>();

            foreach (var assembly in assembliesToLoad)
            {

                Assembly.LoadFrom(assembly);

                //var loader = PluginLoader.CreateFromAssemblyFile(
                //    assembly,
                //    config =>
                //    {
                //        config.PreferSharedTypes = true;
                //        config.LoadInMemory = true;
                //    });
                //loaders.Add(loader);
            }

            foreach (var loader in loaders)
            {
                using (loader.EnterContextualReflection())
                {

                    var defAss = loader.LoadDefaultAssembly();

                    foreach (var referencedAssembly in defAss.GetReferencedAssemblies())
                    {
                        //Assembly.Load(referencedAssembly);
                        loader.LoadAssembly(referencedAssembly);
                    }

                    foreach (var pluginType in defAss
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
