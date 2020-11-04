using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Models.ViewModels
{
    public record EncounterResultViewModel
    {
        public string Name { get; init; }

        public int Order { get; init; }

        public bool Success { get; init; }

        public ReadOnlyCollection<CharacterBase> Characters { get; init; }

        public ReadOnlyCollection<EncounterRoundResult> EncounterRoundResults { get; init; }

        public string ResultDisplayText { get; init; }

        public EncounterResultViewModel(string name, int order, bool success, IEnumerable<EncounterRoundResult> encounterRoundResults, string resultDisplayText, IEnumerable<CharacterBase> characters)
        {
            Name = name;
            Order = order;
            Success = success;
            EncounterRoundResults = encounterRoundResults.ToList().AsReadOnly();
            ResultDisplayText = resultDisplayText;
            Characters = characters.ToList().AsReadOnly();
        }
    }
}
