using DungeonBot.Models.Combat;

namespace DungeonBot.Client.BusinessLogic
{
    public interface ICombatActionProcessor
    {
        ActionResult ProcessAction(IAction action, CharacterBase source, CharacterBase target);
    }
}
