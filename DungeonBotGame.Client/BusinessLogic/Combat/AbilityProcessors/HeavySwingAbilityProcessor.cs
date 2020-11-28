using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.AbilityProcessors
{
    public class HeavySwingAbilityProcessor : IAbilityProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;
        private readonly ICombatValueCalculator _combatValueCalculator;
        private readonly ICombatDamageApplier _combatDamageApplier;

        public HeavySwingAbilityProcessor(ICombatLogEntryBuilder combatLogEntryBuilder, ICombatValueCalculator combatValueCalculator, ICombatDamageApplier combatDamageApplier)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
            _combatValueCalculator = combatValueCalculator;
            _combatDamageApplier = combatDamageApplier;
        }

        public AbilityType AbilityType => AbilityType.HeavySwing;

        public void ProcessAction(IAbilityAction abilityAction, CharacterBase character, CombatContext combatContext)
        {
            if (abilityAction is ITargettedAbilityAction targettedAbilityAction)
            {
                if (targettedAbilityAction.Target is CharacterBase target)
                {
                    var abilityDamage = _combatValueCalculator.GetAttackValue(character, target) * 3;

                    _combatDamageApplier.ApplyDamage(character, target, abilityDamage, combatContext);

                    combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry<IAction>($"{character.Name} took a heavy swing at {target.Name} for {abilityDamage} damage.", character, combatContext, abilityAction));
                }
                else
                {
                    throw new InvalidTargetException(targettedAbilityAction.Target);
                }
            }
            else
            {
                throw new InvalidTargetException((ITarget?)null);
            }
        }
    }
}
