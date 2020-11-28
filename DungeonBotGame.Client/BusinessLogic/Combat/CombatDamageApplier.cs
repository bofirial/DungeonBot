using System.Collections.Generic;
using System.Linq;
using DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface ICombatDamageApplier
    {
        void ApplyDamage(CharacterBase character, CharacterBase target, int combatDamage, CombatContext combatContext);
    }

    public class CombatDamageApplier : ICombatDamageApplier
    {
        private readonly ICombatValueCalculator _combatValueCalculator;
        private readonly ICombatEffectDirector _combatEffectDirector;
        private readonly IDictionary<CombatEffectType, IOnDamageCombatEffectProcessor> _onDamageCombatEffectProcessors;

        public CombatDamageApplier(ICombatValueCalculator combatValueCalculator, ICombatEffectDirector combatEffectDirector, IEnumerable<IOnDamageCombatEffectProcessor> onDamageCombatEffectProcessors)
        {
            _combatValueCalculator = combatValueCalculator;
            _combatEffectDirector = combatEffectDirector;

            _onDamageCombatEffectProcessors = onDamageCombatEffectProcessors.ToDictionary(o => o.CombatEffectType, o => o);
        }

        public void ApplyDamage(CharacterBase character, CharacterBase target, int combatDamage, CombatContext combatContext)
        {
            target.CurrentHealth -= combatDamage;

            _combatValueCalculator.ClampCharacterHealth(target);

            _combatEffectDirector.ProcessCombatEffects(character, _onDamageCombatEffectProcessors,
                (processor, combatEffect) => processor.ProcessCombatEffect(combatEffect, character, target, combatDamage, combatContext));
        }
    }
}
