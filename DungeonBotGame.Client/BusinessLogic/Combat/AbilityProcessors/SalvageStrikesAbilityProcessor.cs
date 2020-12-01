using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.AbilityProcessors
{
    public class SalvageStrikesAbilityProcessor : IPassiveAbilityProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;

        public SalvageStrikesAbilityProcessor(ICombatLogEntryBuilder combatLogEntryBuilder)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
        }

        public AbilityType AbilityType => AbilityType.SalvageStrikes;

        public void ProcessAction(CharacterBase character, CombatContext combatContext)
        {
            character.CombatEffects.Add(new CombatEffect("Salvage Strikes", "Salvage Strikes", CombatEffectType.SalvageStrikes, Value: 1));

            combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry($"{character.Name} prepares salvage strikes.", character, combatContext));
        }
    }
}
