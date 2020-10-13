using System.Collections.Generic;
using System.Linq;
using DungeonBotGame.Models.Display;

namespace DungeonBotGame.Client.Store.Dungeons
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
