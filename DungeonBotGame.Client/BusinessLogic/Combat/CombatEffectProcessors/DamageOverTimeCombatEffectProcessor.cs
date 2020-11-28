using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public class DamageOverTimeCombatEffectProcessor : ICombatEventCombatEffectProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;
        private readonly ICombatValueCalculator _combatValueCalculator;
        private readonly ICombatDamageApplier _combatDamageApplier;

        public DamageOverTimeCombatEffectProcessor(ICombatLogEntryBuilder combatLogEntryBuilder, ICombatValueCalculator combatValueCalculator, ICombatDamageApplier combatDamageApplier)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
            _combatValueCalculator = combatValueCalculator;
            _combatDamageApplier = combatDamageApplier;
        }

        public CombatEffectType CombatEffectType => CombatEffectType.DamageOverTime;

        public void ProcessCombatEventCombatEffect(CombatEvent<CombatEffect> combatEffectEvent, CombatContext combatContext)
        {
            _combatDamageApplier.ApplyDamage(combatEffectEvent.Character, combatEffectEvent.Character, combatEffectEvent.EventData.Value, combatContext, applyCombatEffects: false);

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
