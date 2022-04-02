using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface IPassiveAbilityProcessor
    {
        AbilityType AbilityType { get; }

        void ProcessAction(CharacterBase character, CombatContext combatContext);
    }
}
