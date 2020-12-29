using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using middler.Common.SharedModels.Interfaces;
using middlerApp.Core.DataAccess.Entities.Models;
using middlerApp.Core.Repository;
using middlerApp.Events;
using middlerApp.SharedModels.Interfaces;
using SignalARRR.Attributes;
using SignalARRR.Server;


namespace middlerApp.API.HubMethods
{
    [MessageName("Variables")]
    public class VariablesServerMethods : ServerMethods<UIHub>
    {
        public VariablesRepository VariablesStore { get; }

        public DataEventDispatcher EventDispatcher { get; }

        public VariablesServerMethods(IVariablesRepository variablesStore, DataEventDispatcher eventDispatcher)
        {
            EventDispatcher = eventDispatcher;
            VariablesStore = variablesStore as VariablesRepository;
        }

        public async Task<object> GetFolderTree()
        {
            var tree = await VariablesStore.GetFolderTree();
            return tree;
        }

        public async Task<List<TreeNode>> GetVariablesInParent(string parent)
        {
            return await VariablesStore.GetVariablesInParent(parent);
        }

        public async Task NewFolder(string parent, string name)
        {
            await VariablesStore.NewFolder(parent, name);
        }
        public async Task RenameFolder(string parent, string oldName, string newName)
        {
            await VariablesStore.RenameFolder(parent, oldName, newName);
        }

        public async Task RemoveFolder(string parent, string name)
        {
            await VariablesStore.RemoveFolder(parent, name);
        }


        public async Task<TreeNode> GetVariable(string parent, string name)
        {
            return await VariablesStore.GetVariableAsync(parent, name);
        }

        public async Task UpdateVariableContent(string parent, string name, object content)
        {
            //string contentString = null;
            //switch (content)
            //{
            //    case String str:
            //        {
            //            contentString = content.ToString();
            //            break;
            //        }

            //    default:
            //        {
            //            contentString = Converter.Json.ToJson(content);
            //            break;
            //        }
            //}

            await VariablesStore.UpdateVariableContent(parent, name, content);
        }


        public async Task CreateVariable(TreeNode variable)
        {
            await VariablesStore.CreateVariable(variable);
        }

        public async Task RemoveVariable(string parent, string name)
        {
            await VariablesStore.RemoveVariable(parent, name);
        }

        //public List<KeyValuePair<string, string>> GetTypings()
        //{
        //    var typings =
        //        Directory.GetFileSystemEntries(PathHelper.GetFullPath("TypeDefinitions"))
        //            .Select(fe =>
        //            {
        //                var f = new FileInfo(fe);
        //                return new KeyValuePair<string, string>(f.Name, System.IO.File.ReadAllText(fe));
        //            }).ToList();

        //    return typings;
        //}

        public IObservable<DataEvent> Subscribe()
        {
            return EventDispatcher.Notifications
                .Where(ev => ev.Subject == "Variables");
        }
    }
}
