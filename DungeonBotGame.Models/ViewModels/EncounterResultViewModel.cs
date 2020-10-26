using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Models.ViewModels
{
    public record EncounterResultViewModel
    {
        public bool Success { get; init; }

        public ReadOnlyCollection<EncounterRoundResult> EncounterRoundResults { get; init; }

        public string ResultDisplayText { get; init; }

        public EncounterResultViewModel(bool success, IEnumerable<EncounterRoundResult> encounterRoundResults, string resultDisplayText)
        {
            Success = success;
            EncounterRoundResults = encounterRoundResults.ToList().AsReadOnly();
            ResultDisplayText = resultDisplayText;
        }
    }
}
