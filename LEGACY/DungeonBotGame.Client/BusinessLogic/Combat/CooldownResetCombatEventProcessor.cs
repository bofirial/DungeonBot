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
            if (combatEvent is AbilityCombatEvent abilityCooldownResetEvent)
            {
                combatEvent.Character.Abilities[abilityCooldownResetEvent.AbilityType] = combatEvent.Character.Abilities[abilityCooldownResetEvent.AbilityType] with { IsAvailable = true };
            }
            else
            {
                throw new UnknownCombatEventTypeException("CooldownReset CombatEvents must be CombatEvent<AbilityType>.");
            }

            return Task.CompletedTask;
        }
    }
}
