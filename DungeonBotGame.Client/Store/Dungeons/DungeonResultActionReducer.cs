using System.Collections.Generic;
using DungeonBotGame.Models.Display;
using Fluxor;

namespace DungeonBotGame.Client.Store.Dungeons
{
    public class DungeonResultActionReducer : Reducer<DungeonState, DungeonResultAction>
    {
        public override DungeonState Reduce(DungeonState state, DungeonResultAction action)
        {
            var dungeons = new List<DungeonViewModel>();

            foreach (var dungeon in state.Dungeons)
            {
                if (dungeon == action.Dungeon)
                {
                    var dungeonResults = new List<DungeonResultViewModel>();

                    if (dungeon.DungeonResults != null)
                    {
                        dungeonResults.AddRange(dungeon.DungeonResults);
                    }

                    dungeonResults.Add(action.DungeonResult);

                    dungeons.Add(new DungeonViewModel(dungeon.Name, dungeon.Description, dungeon.Encounters, dungeon.Status, dungeonResults));
                }
                else
                {
                    dungeons.Add(dungeon);
                }
            }

            return new DungeonState(dungeons);
        }
    }
}
