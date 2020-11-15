using System.Collections.Generic;
using System.Collections.Immutable;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public record DungeonBotState
    {
        public IImmutableList<DungeonBotViewModel> DungeonBots { get; init; }

        public DungeonBotState(IEnumerable<DungeonBotViewModel> dungeonBots)
        {
            DungeonBots = dungeonBots.ToImmutableList();
        }
    }
}
