using System.Collections.Immutable;
using Fluxor;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public class SaveDungeonBotActionReducer : Reducer<DungeonBotState, SaveDungeonBotAction>
    {
        public override DungeonBotState Reduce(DungeonBotState state, SaveDungeonBotAction action)
        {
            return state with
            {
                DungeonBots = ImmutableList.Create(action.DungeonBot)
            };
        }
    }
}
