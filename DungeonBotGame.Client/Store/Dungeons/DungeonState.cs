using System.Collections.Generic;
using System.Linq;
using DungeonBotGame.Models.Display;

namespace DungeonBotGame.Client.Store.Dungeons
{
    public class DungeonState
    {
        public IReadOnlyCollection<DungeonViewModel> Dungeons { get; }

        public DungeonState(IEnumerable<DungeonViewModel> dungeons)
        {
            Dungeons = dungeons.ToList().AsReadOnly();
        }
    }
}
