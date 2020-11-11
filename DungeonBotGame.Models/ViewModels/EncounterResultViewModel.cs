using System.Collections.Generic;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Models.ViewModels
{
    public record EncounterResultViewModel(string Name, int Order, bool Success, IReadOnlyCollection<EncounterRoundResult> EncounterRoundResults, string ResultDisplayText, IReadOnlyCollection<CharacterBase> Characters);
}
