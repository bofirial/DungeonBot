using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DungeonBotGame.Models.ViewModels
{
    public record AdventureResultViewModel
    {
        public string RunId { get; init; }

        public bool Success { get; init; }

        public ReadOnlyCollection<EncounterResultViewModel> EncounterResults { get; init; }

        public AdventureResultViewModel(string runId, bool success, IEnumerable<EncounterResultViewModel> encounterResults)
        {
            RunId = runId;
            Success = success;
            EncounterResults = encounterResults.ToList().AsReadOnly();
        }
    }
}
