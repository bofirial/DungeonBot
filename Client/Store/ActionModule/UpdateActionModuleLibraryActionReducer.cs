using System.Collections.Generic;
using System.Linq;
using DungeonBot.Models;
using Fluxor;

namespace DungeonBot.Client.Store.ActionModule
{
    public class UpdateActionModuleLibraryActionReducer : Reducer<ActionModuleState, UpdateActionModuleLibraryAction>
    {
        public override ActionModuleState Reduce(ActionModuleState state, UpdateActionModuleLibraryAction action)
        {
            var actionModuleLibraries = new List<ActionModuleLibrary>() { new ActionModuleLibrary(action.NewActionModuleLibraryName, action.Assembly.ToArray(), action.ActionModuleFiles.ToArray())};

            foreach (var actionModuleLibrary in state.ActionModuleLibraries)
            {
                var str = string.Join(", ", actionModuleLibrary.Assembly);
                if (action.NewActionModuleLibraryName != actionModuleLibrary.Name &&
                    action.PreviousActionModuleLibraryName != actionModuleLibrary.Name &&
                    actionModuleLibraries.Any(a => a.Name == actionModuleLibrary.Name))
                {
                    actionModuleLibraries.Add(actionModuleLibrary);
                }
            }

            return new ActionModuleState(actionModuleLibraries);
        }
    }
}
