using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface ICombatActionProcessor
    {
        ActionResult ProcessAction(IAction action, CharacterBase source);
    }

    public class CombatActionProcessor : ICombatActionProcessor
    {
        private readonly ICombatValueCalculator _combatValueCalculator;

        public CombatActionProcessor(ICombatValueCalculator combatValueCalculator)
        {
            _combatValueCalculator = combatValueCalculator;
        }

        public ActionResult ProcessAction(IAction action, CharacterBase source)
        {
            var actionResult = new ActionResult() { Action = action, Character = source };

            if (action is ITargettedAction targettedAction)
            {
                ProcessTargettedAction(action, source, actionResult, targettedAction);
            }
            else if (action.ActionType == ActionType.Ability && action is IAbilityAction abilityAction)
            {
                ProcessAbilityAction(source, null, actionResult, abilityAction);
            }
            else
            {
                throw new UnknownActionTypeException(action.ActionType);
            }

            if (source.CurrentHealth < 0)
            {
                source.CurrentHealth = 0;
            }

            UpdateAbilityCooldowns(action, source);

            return actionResult;
        }

        private void ProcessTargettedAction(IAction action, CharacterBase source, ActionResult actionResult, ITargettedAction targettedAction)
        {
            if (targettedAction.Target is CharacterBase target)
            {
                if (action.ActionType == ActionType.Attack)
                {
                    ProcessAttackAction(source, target, actionResult);
                }
                else if (action.ActionType == ActionType.Ability && action is IAbilityAction abilityAction)
                {
                    ProcessAbilityAction(source, target, actionResult, abilityAction);
                }
                else
                {
                    throw new UnknownActionTypeException(action.ActionType);
                }

                if (target.CurrentHealth < 0)
                {
                    target.CurrentHealth = 0;
                }
            }
            else
            {
                throw new InvalidTargetException($"Target must be a DungeonBot or Enemy: {targettedAction.Target}");
            }
        }

        private void ProcessAttackAction(CharacterBase source, CharacterBase target, ActionResult actionResult)
        {
            var attackDamage = _combatValueCalculator.GetAttackValue(source, target);

            target.CurrentHealth -= attackDamage;

            actionResult.DisplayText = $"{source.Name} attacked {target.Name} for {attackDamage} damage.";
        }

        private void ProcessAbilityAction(CharacterBase source, CharacterBase? target, ActionResult actionResult, IAbilityAction abilityAction)
        {
            if (!source.Abilities.ContainsKey(abilityAction.AbilityType))
            {
                throw new AbilityNotAvailableException($"{source.Name} does not have access to the ability {abilityAction.AbilityType}.");
            }

            var abilityContext = source.Abilities[abilityAction.AbilityType];

            if (abilityContext.CurrentCooldownRounds > 0)
            {
                throw new AbilityNotAvailableException($"{source.Name} must wait another {abilityContext.CurrentCooldownRounds} rounds before {abilityAction.AbilityType} is available.");
            }

            //TODO: Strategy Pattern for AbilityTypes?
            switch (abilityAction.AbilityType)
            {
                case AbilityType.HeavySwing:

                    if (target == null)
                    {
                        throw new InvalidTargetException(target);
                    }

                    var abilityDamage = _combatValueCalculator.GetAbilityValue(source, target, abilityAction.AbilityType); ;

                    target.CurrentHealth -= abilityDamage;

                    actionResult.DisplayText = $"{source.Name} took a heavy swing at {target.Name} for {abilityDamage} damage.";
                    break;

                case AbilityType.LickWounds:

                    source.CurrentHealth = source.MaximumHealth;

                    actionResult.DisplayText = $"{source.Name} licked it's wounds because a DungeonBot used an ability last turn.";
                    break;

                default:
                    throw new UnknownAbilityTypeException(abilityAction.AbilityType);
            }

            abilityContext.CurrentCooldownRounds = abilityContext.MaximumCooldownRounds;
        }

        private static void UpdateAbilityCooldowns(IAction action, CharacterBase source)
        {
            foreach (var ability in source.Abilities)
            {
                if (ability.Value.CurrentCooldownRounds > 0 && (action is not IAbilityAction abilityAction || ability.Key != abilityAction.AbilityType))
                {
                    ability.Value.CurrentCooldownRounds--;
                }
            }
        }
    }
}
