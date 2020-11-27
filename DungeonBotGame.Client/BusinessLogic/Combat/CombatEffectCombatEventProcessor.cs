using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public class CombatEffectCombatEventProcessor : ICombatEventProcessor
    {
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

                        combatContext.CombatLog.Add(new CombatLogEntry(combatContext.CombatTimer, combatEffectEvent.Character, $"{combatEffectEvent.Character.Name} takes {combatEffectEvent.EventData.Value} damage from {combatEffectEvent.EventData.Name}.", null, combatContext.Characters.Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList()));

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
