using System.Collections.Generic;
using System.Linq;
using DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface ICombatDamageApplier
    {
        void ApplyHealing(CharacterBase character, CharacterBase target, int combatHealing, CombatContext combatContext, bool applyCombatEffects = true);

        void ApplyDamage(CharacterBase character, CharacterBase target, int combatDamage, CombatContext combatContext, bool applyCombatEffects = true);
    }

    public class CombatDamageApplier : ICombatDamageApplier
    {
        private readonly ICombatValueCalculator _combatValueCalculator;
        private readonly ICombatEffectDirector _combatEffectDirector;
        private readonly IDictionary<CombatEffectType, IAfterDamageCombatEffectProcessor> _afterDamageCombatEffectProcessors;
        private readonly IDictionary<CombatEffectType, IAfterHealingCombatEffectProcessor> _afterHealingCombatEffectProcessors;

        public CombatDamageApplier(ICombatValueCalculator combatValueCalculator, ICombatEffectDirector combatEffectDirector, IEnumerable<IAfterDamageCombatEffectProcessor> afterDamageCombatEffectProcessors, IEnumerable<IAfterHealingCombatEffectProcessor> afterHealingCombatEffectProcessors)
        {
            _combatValueCalculator = combatValueCalculator;
            _combatEffectDirector = combatEffectDirector;

            _afterDamageCombatEffectProcessors = afterDamageCombatEffectProcessors.ToDictionary(p => p.CombatEffectType, p => p);
            _afterHealingCombatEffectProcessors = afterHealingCombatEffectProcessors.ToDictionary(p => p.CombatEffectType, p => p);
        }

        public void ApplyDamage(CharacterBase character, CharacterBase target, int combatDamage, CombatContext combatContext, bool applyCombatEffects = true)
        {
            target.CurrentHealth -= combatDamage;

            _combatValueCalculator.ClampCharacterHealth(target);

            if (applyCombatEffects)
            {
                _combatEffectDirector.ProcessCombatEffects(character, _afterDamageCombatEffectProcessors,
                    (processor, combatEffect) => processor.ProcessAfterDamageCombatEffect(combatEffect, character, target, combatDamage, combatContext));
            }
        }

        public void ApplyHealing(CharacterBase character, CharacterBase target, int combatHealing, CombatContext combatContext, bool applyCombatEffects = true)
        {
            target.CurrentHealth += combatHealing;

            _combatValueCalculator.ClampCharacterHealth(target);

            if (applyCombatEffects)
            {
                _combatEffectDirector.ProcessCombatEffects(character, _afterHealingCombatEffectProcessors,
                    (processor, combatEffect) => processor.ProcessAfterHealingCombatEffect(combatEffect, character, target, combatHealing, combatContext));
            }
        }
    }
}
