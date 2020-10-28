using System.Collections.Generic;
using System.Linq;

namespace DungeonBotGame.Models.ViewModels
{
    public record AdventureViewModel
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public IReadOnlyCollection<EncounterViewModel> Encounters { get; init; }

        public string Status { get; init; }

        public IReadOnlyCollection<AdventureResultViewModel> AdventureResults { get; init; }

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
