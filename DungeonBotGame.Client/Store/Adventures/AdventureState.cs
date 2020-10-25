using System.Collections.Generic;
using System.Linq;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.Adventures
{
    public class AdventureState
    {
        public IReadOnlyCollection<AdventureViewModel> Adventures { get; }

        public AdventureState(IEnumerable<AdventureViewModel> adventures)
        {
            Adventures = adventures.ToList().AsReadOnly();
        }
    }
}
