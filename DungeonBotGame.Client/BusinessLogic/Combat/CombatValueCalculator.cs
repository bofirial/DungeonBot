using System;
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
        private readonly IDictionary<CombatEffectType, IAttackValueCombatEffectProcessor> _attackValueCombatEffectProcessors;

        public CombatValueCalculator(ICombatEffectDirector combatEffectDirector, IEnumerable<IIterationsUntilNextActionCombatEffectProcessor> iterationsUntilNextActionCombatEffectProcessors, IEnumerable<IAttackValueCombatEffectProcessor> attackValueCombatEffectProcessors)
        {
            _combatEffectDirector = combatEffectDirector;

            _iterationsUntilNextActionCombatEffectProcessors = iterationsUntilNextActionCombatEffectProcessors.ToDictionary(i => i.CombatEffectType, n => n);
            _attackValueCombatEffectProcessors = attackValueCombatEffectProcessors.ToDictionary(a => a.CombatEffectType, a => a);
        }

        public int GetMaximumHealth(CharacterBase character) => 100 + character.Armor * 5;

        public int GetAttackValue(CharacterBase sourceCharacter, CharacterBase targetCharacter)
        {
            var attackValue = (int)(10 + sourceCharacter.Power * 3.5) - targetCharacter.Armor;

            void ModifyAttackValue(IAttackValueCombatEffectProcessor attackValueCombatEffectProcessor, CombatEffect combatEffect)
            {
                attackValue = attackValueCombatEffectProcessor.ModifyAttackValue(attackValue, combatEffect, sourceCharacter);
            }

            _combatEffectDirector.ProcessCombatEffects(sourceCharacter, _attackValueCombatEffectProcessors, ModifyAttackValue);

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
