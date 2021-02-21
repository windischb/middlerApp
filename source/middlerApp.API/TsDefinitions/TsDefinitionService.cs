using System.Collections.Generic;
using System.IO;
using System.Linq;
using JintTsDefinition;
using middler.Action.Scripting.Models;
using middlerApp.API.Helper;
using Reflectensions.ExtensionMethods;
using Scripter.Shared;

namespace middlerApp.API.TsDefinitions
{
    public class TsDefinitionService
    {
        private readonly IScripterModuleRegistry _scripterModuleRegistry;

        private Dictionary<string, string> Definitions { get; set; }
        private Dictionary<string, string> Imports { get; set; }

        public TsDefinitionService(IScripterModuleRegistry scripterModuleRegistry)
        {
            _scripterModuleRegistry = scripterModuleRegistry;
        }


        public Dictionary<string, string> GetTsDefinitions()
        {
            if (Definitions != null)
            {
                return new Dictionary<string, string>(Definitions);
            }


            var defBuilder = new DefinitionBuilder();

            defBuilder.AddExtensionMethods(typeof(Reflectensions.ExtensionMethods.StringExtensions));
            defBuilder.AddType<EndpointModule>();

            foreach (var md in _scripterModuleRegistry.GetRegisteredModuleDefinitions())
            {

                defBuilder.AddTypes(md.ModuleType);

            }

            Definitions = defBuilder.Render();

            foreach (var file in Directory.GetFiles(PathHelper.GetFullPath("TsDefinitions"), "*.d.ts"))
            {
                var fi = new FileInfo(file);
                Definitions[fi.Name] = File.ReadAllText(file);
            }
            
            return new Dictionary<string, string>(Definitions);
        }

        public Dictionary<string, string> GetTsImports()
        {
            if (Imports != null)
            {
                return new Dictionary<string, string>(Imports);
            }

            Imports = _scripterModuleRegistry.GetRegisteredModuleDefinitions().ToDictionary(
                 md => $"{md.Name}.ts",
                 md =>
                 {
                     var tsr = new TypeScriptRenderer();
                     var td = TypeDefinition.FromType(md.ModuleType);
                     var body = tsr.RenderBody(td, 0, (definition, s) =>
                     {
                         switch (definition)
                         {
                             case MethodDefinition md:
                                 {
                                     return $"export function {s}";
                                 }
                             case PropertyDefinition pd:
                                 {
                                     return $"export const {s}";
                                 }
                         }

                         return s;
                     });
                     return body;
                 });

            return new Dictionary<string, string>(Imports);
        }
    }
}
