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
            var currentDungeonBot = state.DungeonBots.First();

            return state with
            {
                DungeonBots = new List<DungeonBotViewModel>() {
                    currentDungeonBot with { ActionModuleLibrary = new ActionModuleLibraryViewModel(action.Assembly.ToArray(), action.ActionModuleFiles.ToArray()) }
                }
            };
        }
    }
}
