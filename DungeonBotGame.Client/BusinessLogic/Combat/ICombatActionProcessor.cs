using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface ICombatActionProcessor
    {
        ActionResult ProcessAction(IAction action, CharacterBase source, CharacterBase target);
    }
}
