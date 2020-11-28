using System.Collections.Immutable;

namespace DungeonBotGame.Models.Combat
{
    public record CombatLogEntry(
        int CombatTime,
        ICharacter Character,
        string DisplayText,
        IImmutableList<CharacterRecord> Characters) : ICombatLogEntry;

    public record CombatLogEntry<TLogData>(
        int CombatTime,
        ICharacter Character,
        string DisplayText,
        IImmutableList<CharacterRecord> Characters,
        TLogData LogData) : CombatLogEntry(CombatTime, Character, DisplayText, Characters), ICombatLogEntry<TLogData>;
}
