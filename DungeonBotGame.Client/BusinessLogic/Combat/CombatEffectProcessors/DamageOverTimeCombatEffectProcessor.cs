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

        public void ProcessCombatEventCombatEffect(CombatEvent<CombatEffect> combatEffectEvent, CombatContext combatContext)
        {
            if (combatEffectEvent.EventData is CombatEffect<(int CombatTime, int CombatTimeInterval)> dotCombatEffect)
            {
                _combatDamageApplier.ApplyDamage(combatEffectEvent.Character, combatEffectEvent.Character, combatEffectEvent.EventData.Value, combatContext, applyCombatEffects: false);

                if (dotCombatEffect.CombatEffectData.CombatTime <= combatContext.CombatTimer)
                {
                    combatEffectEvent.Character.CombatEffects.Remove(combatEffectEvent.EventData);
                }
                else
                {
                    combatContext.NewCombatEvents.Add(combatEffectEvent with { CombatTime = combatContext.CombatTimer + dotCombatEffect.CombatEffectData.CombatTimeInterval });
                }

                combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry($"{combatEffectEvent.Character.Name} takes {combatEffectEvent.EventData.Value} damage from {combatEffectEvent.EventData.Name}.", combatEffectEvent.Character, combatContext, combatEffectEvent.EventData));
            }
        }
    }
}
