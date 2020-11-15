using System.Collections.Immutable;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Models.ViewModels
{
    public record EncounterResultViewModel(
        string Name,
        int Order,
        bool Success,
        IImmutableList<EncounterRoundResult> EncounterRoundResults,
        string ResultDisplayText,
        IImmutableList<CharacterBase> Characters);
}
