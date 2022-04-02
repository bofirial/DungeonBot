using System.Threading.Tasks;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface ICombatEventProcessor
    {
        CombatEventType CombatEventType { get; }

        Task ProcessCombatEvent(CombatEvent combatEvent, CombatContext combatContext);
    }
}
