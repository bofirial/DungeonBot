using System.Collections.Generic;
using System.Collections.Immutable;
using DungeonBotGame.Models.ViewModels;
using Fluxor;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public class SaveDungeonBotActionReducer : Reducer<DungeonBotState, SaveDungeonBotAction>
    {
        public override DungeonBotState Reduce(DungeonBotState state, SaveDungeonBotAction action)
        {
            var dungeonBots = new List<DungeonBotViewModel>();

            foreach (var dungeonBot in state.DungeonBots)
            {
                if (dungeonBot.Id == action.DungeonBot.Id)
                {
                    dungeonBots.Add(action.DungeonBot);
                }
                else
                {
                    dungeonBots.Add(dungeonBot);
                }
            }

            return state with
            {
                DungeonBots = dungeonBots.ToImmutableList(),
                IsSaving = false
            };
        }
    }
}
