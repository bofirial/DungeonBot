using System.Collections.Generic;
using System.Linq;

namespace DungeonBotGame.Models.ViewModels
{
    public class AdventureViewModel
    {
        public string Name { get; }

        public string Description { get; }

        public IReadOnlyCollection<EncounterViewModel> Encounters { get; }

        public string Status { get; }

        public IReadOnlyCollection<AdventureResultViewModel> AdventureResults { get; }

        public AdventureViewModel(string name, string description, IEnumerable<EncounterViewModel> encounters, string status, IEnumerable<AdventureResultViewModel> adventureResults)
        {
            Name = name;
            Description = description;
            Encounters = encounters.ToList().AsReadOnly();
            Status = status;
            AdventureResults = adventureResults?.ToList().AsReadOnly();
        }
    }
}
