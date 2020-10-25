namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface ICombatValueCalculator
    {
        int GetAttackValue(ICharacter sourceCharacter, ICharacter targetCharacter);

        int GetAbilityValue(ICharacter sourceCharacter, ICharacter targetCharacter, AbilityType abilityType);
    }
}
