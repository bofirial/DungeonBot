using System.Collections.Immutable;

namespace DungeonBotGame.Models.Combat
{
    public record CombatLogEntry(
        int CombatTime,
        ICharacter Character,
        string DisplayText,
        IAction Action,
        IImmutableList<CharacterRecord> Characters) : ICombatLogEntry;
}
