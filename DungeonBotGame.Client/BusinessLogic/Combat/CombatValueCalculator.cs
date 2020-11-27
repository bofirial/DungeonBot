using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface ICombatValueCalculator
    {
        int GetMaximumHealth(CharacterBase character);

        int GetAttackValue(CharacterBase sourceCharacter, CharacterBase targetCharacter);

        int GetIterationsUntilNextAction(CharacterBase character);

        void ClampCharacterHealth(CharacterBase character);
    }

    public class CombatValueCalculator : ICombatValueCalculator
    {
        private readonly ICombatEffectDirector _combatEffectDirector;

        private readonly IDictionary<CombatEffectType, IIterationsUntilNextActionCombatEffectProcessor> _iterationsUntilNextActionCombatEffectProcessors;

        public CombatValueCalculator(ICombatEffectDirector combatEffectDirector, IEnumerable<IIterationsUntilNextActionCombatEffectProcessor> iterationsUntilNextActionCombatEffectProcessors)
        {
            _combatEffectDirector = combatEffectDirector;

            _iterationsUntilNextActionCombatEffectProcessors = iterationsUntilNextActionCombatEffectProcessors.ToDictionary(n => n.CombatEffectType, n => n);
        }

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
            var iterationsUntilNextAction = 300 - 6 * character.Speed;

            void ModifyIterationsUntilNextAction(IIterationsUntilNextActionCombatEffectProcessor iterationsUntilNextActionCombatEffectProcessor, CombatEffect combatEffect)
            {
                iterationsUntilNextAction = iterationsUntilNextActionCombatEffectProcessor.ModifyIterationsUntilNextAction(iterationsUntilNextAction, combatEffect, character);
            }

            _combatEffectDirector.ProcessCombatEffects(character, _iterationsUntilNextActionCombatEffectProcessors, ModifyIterationsUntilNextAction);

            return iterationsUntilNextAction;
        }

        public void ClampCharacterHealth(CharacterBase character) =>
            character.CurrentHealth = Math.Clamp(character.CurrentHealth, 0, character.MaximumHealth);
    }
}
