using System.Collections.Generic;
using DungeonBot.Models.Display;
using Fluxor;

namespace DungeonBot.Client.Store.Dungeons
{
    public class DungeonResultActionReducer : Reducer<DungeonState, DungeonResultAction>
    {
        public override DungeonState Reduce(DungeonState state, DungeonResultAction action)
        {
            var dungeons = new List<Dungeon>();

            foreach (var dungeon in state.Dungeons)
            {
                if (dungeon == action.Dungeon)
                {
                    var dungeonResults = new List<DungeonResult>();

                    if (dungeon.DungeonResults != null)
                    {
                        dungeonResults.AddRange(dungeon.DungeonResults);
                    }

                    dungeonResults.Add(action.DungeonResult);

                    dungeons.Add(new Dungeon(dungeon.Name, dungeon.Description, dungeon.Encounters, dungeon.Status, dungeonResults));
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
