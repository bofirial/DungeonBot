using System.Collections.Generic;
using System.Linq;
using DungeonBotGame.Models.ViewModels;
using Fluxor;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public class UpdateActionModuleLibraryActionReducer : Reducer<DungeonBotState, UpdateActionModuleLibraryAction>
    {
        public override DungeonBotState Reduce(DungeonBotState state, UpdateActionModuleLibraryAction action)
        {
            var actionModuleLibraries = new List<ActionModuleLibraryViewModel>() { new ActionModuleLibraryViewModel(action.NewActionModuleLibraryName, action.Assembly.ToArray(), action.ActionModuleFiles.ToArray())};

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

            return new DungeonBotState(actionModuleLibraries);
        }
    }
}
