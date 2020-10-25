using DungeonBotGame.Client.ErrorHandling;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public class CombatValueCalculator : ICombatValueCalculator
    {
        public int GetAttackValue(ICharacter sourceCharacter, ICharacter targetCharacter)
        {
            if (sourceCharacter.Name == "Hungry Dragon Whelp" || sourceCharacter.Name == "Wolf King")
            {
                return 15;
            }

            return 10;
        }

        public int GetAbilityValue(ICharacter sourceCharacter, ICharacter targetCharacter, AbilityType abilityType)
        {
            return abilityType switch
            {
                AbilityType.HeavySwing => GetAttackValue(sourceCharacter, targetCharacter) * 3,
                AbilityType.LickWounds => targetCharacter.MaximumHealth,
                _ => throw new UnknownAbilityException(abilityType)
            };
        }
    }
}
