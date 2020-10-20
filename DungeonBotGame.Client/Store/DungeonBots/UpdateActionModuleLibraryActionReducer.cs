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

            var newDungeonBot = new DungeonBotViewModel(
                currentDungeonBot.Id,
                currentDungeonBot.Name,
                currentDungeonBot.ProfileImageLocation,
                new ActionModuleLibraryViewModel(action.NewActionModuleLibraryName, action.Assembly.ToArray(), action.ActionModuleFiles.ToArray()),
                currentDungeonBot.Abilities.ToList()
                );

            return new DungeonBotState(new List<DungeonBotViewModel>() { newDungeonBot });
        }
    }
}
