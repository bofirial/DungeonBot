namespace DungeonBot.Client.BusinessLogic
{
    public interface ICombatValueCalculator
    {
        int GetAttackValue(ICharacter sourceCharacter, ICharacter targetCharacter);

        int GetAbilityValue(ICharacter sourceCharacter, ICharacter targetCharacter, AbilityType abilityType);
    }
}
