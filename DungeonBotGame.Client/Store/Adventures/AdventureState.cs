using System.Collections.Generic;
using System.Collections.Immutable;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.Adventures
{
    public record AdventureState
    {
        public IImmutableList<AdventureViewModel> Adventures { get; init; }

        public AdventureState(IEnumerable<AdventureViewModel> adventures)
        {
            Adventures = adventures.ToImmutableList();
        }
    }
}
