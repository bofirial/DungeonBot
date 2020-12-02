using System.Linq;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.AbilityProcessors
{
    public class SwipeAbilityProcessor : IAbilityProcessor
    {
        private readonly ICombatValueCalculator _combatValueCalculator;
        private readonly ICombatDamageApplier _combatDamageApplier;
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;

        public SwipeAbilityProcessor(ICombatValueCalculator combatValueCalculator, ICombatDamageApplier combatDamageApplier, ICombatLogEntryBuilder combatLogEntryBuilder)
        {
            _combatValueCalculator = combatValueCalculator;
            _combatDamageApplier = combatDamageApplier;
            _combatLogEntryBuilder = combatLogEntryBuilder;
        }

        public AbilityType AbilityType => AbilityType.Swipe;

        public void ProcessAction(IAbilityAction abilityAction, CharacterBase character, CombatContext combatContext)
        {
            foreach (var target in combatContext.Characters.Where(c => c is DungeonBot != character is DungeonBot && c.CurrentHealth > 0))
            {
                var abilityDamage = _combatValueCalculator.GetAttackValue(character, target);

                _combatDamageApplier.ApplyDamage(character, target, abilityDamage, combatContext);

                combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry<IAction>(
                    $"{character.Name} swiped at {target.Name} for {abilityDamage} damage.", character, combatContext, abilityAction));
            }

        }
    }
}
