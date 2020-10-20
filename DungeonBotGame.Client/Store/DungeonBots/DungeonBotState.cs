using System.Collections.Generic;
using System.Linq;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public class DungeonBotState
    {
        public IReadOnlyCollection<DungeonBotViewModel> DungeonBots { get; }

        public DungeonBotState(IEnumerable<DungeonBotViewModel> dungeonBots)
        {
            DungeonBots = dungeonBots.ToList().AsReadOnly();
        }
    }
}
