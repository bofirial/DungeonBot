using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface IActionProcessor
    {
        ActionType ActionType { get; }

        void ProcessAction(IAction action, CharacterBase character, CombatContext combatContext);
    }
}
