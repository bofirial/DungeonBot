using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface IAbilityProcessor
    {
        AbilityType AbilityType { get; }

        void ProcessAction(IAbilityAction abilityAction, CharacterBase character, CombatContext combatContext);
    }
}
