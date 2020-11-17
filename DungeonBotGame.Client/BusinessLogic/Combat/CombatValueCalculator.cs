using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface ICombatValueCalculator
    {
        int GetMaximumHealth(CharacterBase character);

        int GetAttackValue(CharacterBase sourceCharacter, CharacterBase targetCharacter);

        int GetAbilityValue(CharacterBase sourceCharacter, CharacterBase targetCharacter, AbilityType abilityType);
    }

    public class CombatValueCalculator : ICombatValueCalculator
    {

        public int GetMaximumHealth(CharacterBase character) => 50 + character.Armor * 10;

        public int GetAttackValue(CharacterBase sourceCharacter, CharacterBase targetCharacter)
        {
            if (sourceCharacter.Name == "Hungry Dragon Whelp" || sourceCharacter.Name == "Wolf King")
            {
                return 15;
            }

            return 10;
        }

        public int GetAbilityValue(CharacterBase sourceCharacter, CharacterBase targetCharacter, AbilityType abilityType)
        {
            return abilityType switch
            {
                AbilityType.HeavySwing => GetAttackValue(sourceCharacter, targetCharacter) * 3,
                AbilityType.LickWounds => targetCharacter.MaximumHealth,
                _ => throw new UnknownAbilityTypeException(abilityType)
            };
        }
    }
}
