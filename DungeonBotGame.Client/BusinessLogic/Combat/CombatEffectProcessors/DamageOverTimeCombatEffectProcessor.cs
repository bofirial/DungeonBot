using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public class DamageOverTimeCombatEffectProcessor : ICombatEventCombatEffectProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;
        private readonly ICombatDamageApplier _combatDamageApplier;

        public DamageOverTimeCombatEffectProcessor(ICombatLogEntryBuilder combatLogEntryBuilder, ICombatDamageApplier combatDamageApplier)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
            _combatDamageApplier = combatDamageApplier;
        }

        public CombatEffectType CombatEffectType => CombatEffectType.DamageOverTime;

        public void ProcessCombatEventCombatEffect(CombatEffectCombatEvent combatEffectEvent, CombatContext combatContext)
        {
            if (combatEffectEvent.CombatEffect is TimedIntervalCombatEffect dotCombatEffect)
            {
                _combatDamageApplier.ApplyDamage(combatEffectEvent.Character, combatEffectEvent.Character, combatEffectEvent.CombatEffect.Value, combatContext, applyCombatEffects: false, applyArmorDamageReduction: false);

                if (dotCombatEffect.CombatTime <= combatContext.CombatTimer)
                {
                    combatEffectEvent.Character.CombatEffects.Remove(combatEffectEvent.CombatEffect);
                }
                else
                {
                    combatContext.NewCombatEvents.Add(combatEffectEvent with { CombatTime = combatContext.CombatTimer + dotCombatEffect.CombatTimeInterval });
                }

                combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry($"{combatEffectEvent.Character.Name} takes {combatEffectEvent.CombatEffect.Value} damage from {combatEffectEvent.CombatEffect.Name}.", combatEffectEvent.Character, combatContext, combatEffectEvent.CombatEffect));
            }
        }
    }
}
