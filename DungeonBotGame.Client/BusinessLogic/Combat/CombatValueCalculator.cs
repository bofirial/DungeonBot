using System.Linq;
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

        public int GetAttackValue(CharacterBase sourceCharacter, CharacterBase targetCharacter)
        {
            var attackValue = (int)(10 + sourceCharacter.Power * 3.5) - targetCharacter.Armor;

            var attackModifierCombatEffects = sourceCharacter.CombatEffects.Where(c => c.CombatEffectType == CombatEffectType.AttackPercentage).ToList();

            foreach (var attackModifierCombatEffect in attackModifierCombatEffects)
            {
                attackValue = (int)(attackValue * (attackModifierCombatEffect.Value / 100.0 ));

                sourceCharacter.CombatEffects.Remove(attackModifierCombatEffect);
            }

            return attackValue;
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

        public int GetIterationsUntilNextAction(CharacterBase character)
        {
            var iterationsUntilNextAction = 300 - 6 * character.Speed;

            var iterationsUntilNextActionModifierCombatEffects = character.CombatEffects.Where(c => c.CombatEffectType == CombatEffectType.ActionCombatTimePercentage).ToList();

            foreach (var iterationsUntilNextActionModifierCombatEffect in iterationsUntilNextActionModifierCombatEffects)
            {
                iterationsUntilNextAction = (int)(iterationsUntilNextAction * (iterationsUntilNextActionModifierCombatEffect.Value / 100.0 ));

                character.CombatEffects.Remove(iterationsUntilNextActionModifierCombatEffect);
            }

            return iterationsUntilNextAction;
        }
    }
}
