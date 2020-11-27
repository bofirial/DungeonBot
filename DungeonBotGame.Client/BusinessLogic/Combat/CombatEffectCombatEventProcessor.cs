using System.Threading.Tasks;
using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public class CombatEffectCombatEventProcessor : ICombatEventProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;

        public CombatEffectCombatEventProcessor(ICombatLogEntryBuilder combatLogEntryBuilder)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
        }

        public CombatEventType CombatEventType => CombatEventType.CombatEffect;

        public Task ProcessCombatEvent(CombatEvent combatEvent, CombatContext combatContext)
        {
            if (combatEvent is CombatEvent<CombatEffect> combatEffectEvent)
            {
                switch (combatEffectEvent.EventData.CombatEffectType)
                {
                    case CombatEffectType.DamageOverTime:

                        combatEffectEvent.Character.CurrentHealth -= combatEffectEvent.EventData.Value;

                        if (combatEffectEvent.EventData.CombatTime <= combatContext.CombatTimer)
                        {
                            combatEffectEvent.Character.CombatEffects.Remove(combatEffectEvent.EventData);
                        }
                        else if (combatEffectEvent.EventData.CombatTimeInterval != null)
                        {
                            combatContext.NewCombatEvents.Add(combatEffectEvent with { CombatTime = combatContext.CombatTimer + combatEffectEvent.EventData.CombatTimeInterval.Value });
                        }

                        combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry($"{combatEffectEvent.Character.Name} takes {combatEffectEvent.EventData.Value} damage from {combatEffectEvent.EventData.Name}.", combatEffectEvent.Character, combatContext, combatEffectEvent.EventData));

                        break;
                }
            }
            else
            {
                throw new UnknownCombatEventTypeException("CombatEffect CombatEvents must be CombatEvent<CombatEffect>.");
            }

            return Task.CompletedTask;
        }
    }
}
