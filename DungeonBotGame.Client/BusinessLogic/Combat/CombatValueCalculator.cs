using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface ICombatValueCalculator
    {
        int GetMaximumHealth(CharacterBase character);

        int GetAttackValue(CharacterBase sourceCharacter, CharacterBase targetCharacter);

        int GetAbilityValue(CharacterBase sourceCharacter, CharacterBase targetCharacter, AbilityType abilityType);

        int GetIterationsUntilNextAction(CharacterBase character);
    }

    public class CombatValueCalculator : ICombatValueCalculator
    {

        public int GetMaximumHealth(CharacterBase character) => 100 + character.Armor * 5;

        public int GetAttackValue(CharacterBase sourceCharacter, CharacterBase targetCharacter) => (int)(10 + sourceCharacter.Power * 3.5) - targetCharacter.Armor;

        public int GetAbilityValue(CharacterBase sourceCharacter, CharacterBase targetCharacter, AbilityType abilityType)
        {
            return abilityType switch
            {
                AbilityType.HeavySwing => GetAttackValue(sourceCharacter, targetCharacter) * 3,
                AbilityType.LickWounds => targetCharacter.MaximumHealth,
                _ => throw new UnknownAbilityTypeException(abilityType)
            };
        }

        public int GetIterationsUntilNextAction(CharacterBase character) => 300 - 6 * character.Speed;
    }
}
