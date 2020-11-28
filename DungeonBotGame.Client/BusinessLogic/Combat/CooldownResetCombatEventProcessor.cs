using System.Threading.Tasks;
using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public class CooldownResetCombatEventProcessor : ICombatEventProcessor
    {
        public CombatEventType CombatEventType => CombatEventType.CooldownReset;

        public Task ProcessCombatEvent(CombatEvent combatEvent, CombatContext combatContext)
        {
            if (combatEvent is CombatEvent<AbilityType> abilityCooldownResetEvent)
            {
                combatEvent.Character.Abilities[abilityCooldownResetEvent.EventData] = combatEvent.Character.Abilities[abilityCooldownResetEvent.EventData] with { IsAvailable = true };
            }
            else
            {
                throw new UnknownCombatEventTypeException("CooldownReset CombatEvents must be CombatEvent<AbilityType>.");
            }

            return Task.CompletedTask;
        }
    }
}
