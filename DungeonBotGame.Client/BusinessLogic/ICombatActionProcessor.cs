using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic
{
    public interface ICombatActionProcessor
    {
        ActionResult ProcessAction(IAction action, CharacterBase source, CharacterBase target);
    }
}
