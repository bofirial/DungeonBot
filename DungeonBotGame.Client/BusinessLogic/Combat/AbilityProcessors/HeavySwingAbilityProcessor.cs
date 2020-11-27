using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.AbilityProcessors
{
    public class HeavySwingAbilityProcessor : IAbilityProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;
        private readonly ICombatValueCalculator _combatValueCalculator;

        public HeavySwingAbilityProcessor(ICombatLogEntryBuilder combatLogEntryBuilder, ICombatValueCalculator combatValueCalculator)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
            _combatValueCalculator = combatValueCalculator;
        }

        public AbilityType AbilityType => AbilityType.HeavySwing;

        public void ProcessAction(IAbilityAction abilityAction, CharacterBase character, CombatContext combatContext)
        {
            if (abilityAction is ITargettedAbilityAction targettedAbilityAction)
            {
                if (targettedAbilityAction.Target is CharacterBase target)
                {
                    var abilityDamage = _combatValueCalculator.GetAttackValue(character, target) * 3;

                    target.CurrentHealth -= abilityDamage;

                    _combatValueCalculator.ClampCharacterHealth(target);

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
