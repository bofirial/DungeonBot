using System.Collections.Immutable;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public record DungeonBotState(IImmutableList<DungeonBotViewModel> DungeonBots);
}
