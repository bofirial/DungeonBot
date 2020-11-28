using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public class DamageOverTimeCombatEffectProcessor : ICombatEventCombatEffectProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;
        private readonly ICombatValueCalculator _combatValueCalculator;

        public DamageOverTimeCombatEffectProcessor(ICombatLogEntryBuilder combatLogEntryBuilder, ICombatValueCalculator combatValueCalculator)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
            _combatValueCalculator = combatValueCalculator;
        }

        public CombatEffectType CombatEffectType => CombatEffectType.DamageOverTime;

        public void ProcessCombatEvent(CombatEvent<CombatEffect> combatEffectEvent, CombatContext combatContext)
        {
            combatEffectEvent.Character.CurrentHealth -= combatEffectEvent.EventData.Value;

            _combatValueCalculator.ClampCharacterHealth(combatEffectEvent.Character);

            if (combatEffectEvent.EventData.CombatTime <= combatContext.CombatTimer)
            {
                combatEffectEvent.Character.CombatEffects.Remove(combatEffectEvent.EventData);
            }
            else if (combatEffectEvent.EventData.CombatTimeInterval != null)
            {
                combatContext.NewCombatEvents.Add(combatEffectEvent with { CombatTime = combatContext.CombatTimer + combatEffectEvent.EventData.CombatTimeInterval.Value });
            }

            combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry($"{combatEffectEvent.Character.Name} takes {combatEffectEvent.EventData.Value} damage from {combatEffectEvent.EventData.Name}.", combatEffectEvent.Character, combatContext, combatEffectEvent.EventData));
        }
    }
}
