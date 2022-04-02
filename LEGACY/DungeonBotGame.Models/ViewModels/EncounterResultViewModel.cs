using System.Collections.Immutable;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Models.ViewModels
{
    public record EncounterResultViewModel(
        string Name,
        int Order,
        bool Success,
        IImmutableList<CombatLogEntry> CombatLog,
        string ResultDisplayText,
        IImmutableList<CharacterBase> Characters);
}
