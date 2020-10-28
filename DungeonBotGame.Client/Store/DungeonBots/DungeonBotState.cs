using System.Collections.Generic;
using System.Linq;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public record DungeonBotState
    {
        public IReadOnlyCollection<DungeonBotViewModel> DungeonBots { get; init; }

        public DungeonBotState(IEnumerable<DungeonBotViewModel> dungeonBots)
        {
            DungeonBots = dungeonBots.ToList().AsReadOnly();
        }
    }
}
