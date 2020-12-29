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
using middlerApp.Agents.Shared;
using middlerApp.API.Helper;
using Reflectensions.ExtensionMethods;
using Reflectensions.HelperClasses;
using Scripter.Shared;

namespace middlerApp.API.ExtensionMethods
{
    public static class IScripterContextExtensions
    {
        public static Dictionary<string, string> TsDefinitions { get; set; } = new Dictionary<string, string>();
        public static Dictionary<string, string> TsImports { get; set; } = new Dictionary<string, string>();

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
                Assembly.LoadFrom(file);
            }

            foreach (var directory in Directory.GetDirectories(dir))
            {
                //foreach (var file in Directory.GetFiles(directory, "*.dll"))
                //{
                //    tsDefinitionAssemblies.Add(file);
                //}

                //foreach (var file in Directory.GetFiles(directory))
                //{
                //    tsDefinitionAssemblies.Add(file);

                //}

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

            var defBuilder = new DefinitionBuilder();
            defBuilder.AddType<Guid>();
            defBuilder.AddExtensionMethods(typeof(StringExtensions));
            defBuilder.AddTypes(typeof(Task<>));
            // Create an instance of plugin types
            foreach (var loader in loaders)
            {
                using (loader.EnterContextualReflection())
                {
                    var dass = loader.LoadDefaultAssembly();
                    defBuilder.AddTypesFromAssembly(dass);
                    foreach (var referencedAssembly in dass.GetReferencedAssemblies())
                    {

                        var ass = Assembly.Load(referencedAssembly);
                        defBuilder.AddTypesFromAssembly(ass);
                    }

                    foreach (var pluginType in loader
                        .LoadDefaultAssembly()
                        .GetTypes()
                        .Where(t => typeof(IScripterModule).IsAssignableFrom(t) && !t.IsAbstract))
                    {
                        var tsr = new TypeScriptRenderer();

                        var tp = TypeDefinition.FromType(pluginType);

                        var body = tsr.RenderBody(tp, 0, (definition, s) =>
                        {
                            switch (definition)
                            {
                                case MethodDefinition md:
                                {
                                    return $"export function {s}";
                                }
                                case PropertyDefinition pd:
                                {
                                    return $"export declare const {s}";
                                }
                            }

                            return s;
                        });
                        
                        //var mds = pluginType
                        //    .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                        //    .Where(m => !m.IsSpecialName)
                        //    .Select(MethodDefinition.FromMethodInfo)
                        //    .Select(m => $"export function {tsr.Render(m, 0)}");
                        var name = pluginType.Name;
                        if (name.EndsWith("Module"))
                        {
                            name = name.Substring(0, name.Length - "Module".Length);
                        }
                        TsImports.Add($"{name}.ts",body);

                        context.AddScripterModule(pluginType);
                    }
                }

                
            }

            

            //foreach (var tsDefinitionAssembly in tsDefinitionAssemblies)
            //{
            //    var ass = Assembly.ReflectionOnlyLoadFrom(tsDefinitionAssembly);
            //    foreach (var type in ass.GetTypes())
            //    {
            //        if (!type.IsPublic)
            //        {
            //            continue;
            //        }

            //        defBuilder.AddTypes(type);
            //    }
            //}

            TsDefinitions = defBuilder.Render();



            foreach (var (key, value) in TsDefinitions)
            {
                File.WriteAllText($"D:\\tstest\\{key}", value);
            }

            return context;
        }
    }
}
