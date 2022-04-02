using System.Collections.Immutable;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.Adventures
{
    public record RunAdventureAction(AdventureViewModel Adventure, IImmutableList<DungeonBotViewModel> DungeonBots, string RunId);
}
