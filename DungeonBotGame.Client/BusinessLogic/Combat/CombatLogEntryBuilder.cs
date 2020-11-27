using System.Collections.Immutable;
using System.Linq;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface ICombatLogEntryBuilder
    {
        CombatLogEntry CreateCombatLogEntry(string displayText, CharacterBase character,  CombatContext combatContext);

        CombatLogEntry<TLogData> CreateCombatLogEntry<TLogData>(string displayText, CharacterBase character, CombatContext combatContext, TLogData logData);
    }

    public class CombatLogEntryBuilder : ICombatLogEntryBuilder
    {
        private static ImmutableList<CharacterRecord> BuildCharacterRecords(CombatContext combatContext) =>
            combatContext.Characters.Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList();

        public CombatLogEntry CreateCombatLogEntry(string displayText, CharacterBase character, CombatContext combatContext) => new(
                    combatContext.CombatTimer,
                    character,
                    displayText,
                    BuildCharacterRecords(combatContext));

        public CombatLogEntry<TLogData> CreateCombatLogEntry<TLogData>(string displayText, CharacterBase character, CombatContext combatContext, TLogData logData) => new(
                    combatContext.CombatTimer,
                    character,
                    displayText,
                    BuildCharacterRecords(combatContext),
                    logData);
    }
}
