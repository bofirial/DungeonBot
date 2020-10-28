using System.Collections.Generic;
using System.Linq;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.Adventures
{
    public record AdventureState
    {
        public IReadOnlyCollection<AdventureViewModel> Adventures { get; init; }

        public AdventureState(IEnumerable<AdventureViewModel> adventures)
        {
            Adventures = adventures.ToList().AsReadOnly();
        }
    }
}
