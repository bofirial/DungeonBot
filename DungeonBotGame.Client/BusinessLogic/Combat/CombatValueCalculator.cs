using System.Linq;
using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface ICombatValueCalculator
    {
        int GetMaximumHealth(CharacterBase character);

        int GetAttackValue(CharacterBase sourceCharacter, CharacterBase targetCharacter);

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

        public int GetIterationsUntilNextAction(CharacterBase character)
        {
            var iterationsUntilNextCharacterAction = 300 - 6 * character.Speed;

            var iterationsUntilNextActionCombatEffectTypes = new CombatEffectType[] { CombatEffectType.ActionCombatTimePercentage, CombatEffectType.ImmediateAction };

            var iterationsUntilNextActionModifierCombatEffects = character.CombatEffects.Where(c => iterationsUntilNextActionCombatEffectTypes.Contains(c.CombatEffectType)).ToList();

            foreach (var iterationsUntilNextActionModifierCombatEffect in iterationsUntilNextActionModifierCombatEffects)
            {
                switch (iterationsUntilNextActionModifierCombatEffect.CombatEffectType)
                {
                    case CombatEffectType.ActionCombatTimePercentage:

                        iterationsUntilNextCharacterAction = (int)(iterationsUntilNextCharacterAction * (iterationsUntilNextActionModifierCombatEffect.Value / 100.0));

                        character.CombatEffects.Remove(iterationsUntilNextActionModifierCombatEffect);
                        break;

                    case CombatEffectType.ImmediateAction:
                        iterationsUntilNextCharacterAction = 1;

                        character.CombatEffects.Remove(iterationsUntilNextActionModifierCombatEffect);
                        break;
                }
            }

            return iterationsUntilNextCharacterAction;
        }
    }
}
