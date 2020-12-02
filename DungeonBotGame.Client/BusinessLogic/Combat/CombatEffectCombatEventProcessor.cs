using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors;
using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public class CombatEffectCombatEventProcessor : ICombatEventProcessor
    {
        private readonly IDictionary<CombatEffectType, ICombatEventCombatEffectProcessor> _combatEventCombatEffectProcessors;

        public CombatEffectCombatEventProcessor(IEnumerable<ICombatEventCombatEffectProcessor> combatEventCombatEffectProcessors)
        {
            _combatEventCombatEffectProcessors = combatEventCombatEffectProcessors.ToDictionary(p => p.CombatEffectType, p => p);
        }

        public CombatEventType CombatEventType => CombatEventType.CombatEffect;

        public Task ProcessCombatEvent(CombatEvent combatEvent, CombatContext combatContext)
        {
            if (combatEvent is CombatEffectCombatEvent combatEffectEvent)
            {
                if (_combatEventCombatEffectProcessors.ContainsKey(combatEffectEvent.CombatEffect.CombatEffectType))
                {
                    _combatEventCombatEffectProcessors[combatEffectEvent.CombatEffect.CombatEffectType].ProcessCombatEventCombatEffect(combatEffectEvent, combatContext);
                }
                else
                {
                    throw new UnknownCombatEventTypeException($"No CombatEventCombatEffectProcessor found for the CombatEffectEvent {combatEffectEvent.CombatEffect.CombatEffectType}.");
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
