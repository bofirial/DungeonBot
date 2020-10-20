using System.Collections.Generic;
using System.Linq;

namespace DungeonBotGame.Models.ViewModels
{
    public class DungeonViewModel
    {
        public string Name { get; }

        public string Description { get; }

        public IReadOnlyCollection<EncounterViewModel> Encounters { get; }

        public string Status { get; }

        public IReadOnlyCollection<DungeonResultViewModel> DungeonResults { get; }

        public DungeonViewModel(string name, string description, IEnumerable<EncounterViewModel> encounters, string status, IEnumerable<DungeonResultViewModel> dungeonResults)
        {
            Name = name;
            Description = description;
            Encounters = encounters.ToList().AsReadOnly();
            Status = status;
            DungeonResults = dungeonResults?.ToList().AsReadOnly();
        }
    }
}
