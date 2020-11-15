using System.Collections.Generic;
using DungeonBotGame.Models.ViewModels;
using Fluxor;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public class SaveDungeonBotActionReducer : Reducer<DungeonBotState, SaveDungeonBotAction>
    {
        public override DungeonBotState Reduce(DungeonBotState state, SaveDungeonBotAction action)
        {
            return state with
            {
                DungeonBots = new List<DungeonBotViewModel>() { action.DungeonBot }
            };
        }
    }
}
