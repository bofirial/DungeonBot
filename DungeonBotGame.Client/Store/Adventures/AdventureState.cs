using System.Collections.Immutable;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.Adventures
{
    public record AdventureState(IImmutableList<AdventureViewModel> Adventures);
}
