using System.Collections.Generic;
using System.Linq;
using DungeonBot.Models.Display;

namespace DungeonBot.Client.Store.ActionModule
{
    public class DungeonState
    {
        public IReadOnlyCollection<Dungeon> Dungeons { get; }

        public DungeonState(IEnumerable<Dungeon> dungeons)
        {
            Dungeons = dungeons.ToList().AsReadOnly();
        }
    }
}
