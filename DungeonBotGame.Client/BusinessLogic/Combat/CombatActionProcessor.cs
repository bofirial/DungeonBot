using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface ICombatActionProcessor
    {
        IImmutableList<ActionResult> ProcessAction(IAction action, CharacterBase source, int combatTime, IImmutableList<CharacterBase> characters);
    }

    public class CombatActionProcessor : ICombatActionProcessor
    {
        private readonly ICombatValueCalculator _combatValueCalculator;

        public CombatActionProcessor(ICombatValueCalculator combatValueCalculator)
        {
            _combatValueCalculator = combatValueCalculator;
        }

        public IImmutableList<ActionResult> ProcessAction(IAction action, CharacterBase source, int combatTime, IImmutableList<CharacterBase> characters)
        {
            var actionResult = new ActionResult(combatTime, source, string.Empty, action, ImmutableList.Create<CharacterRecord>(), ImmutableList.Create<CombatEvent>());

            var fallenCharactersBefore = characters.Where(c => c.CurrentHealth <= 0).ToList();

            if (action is ITargettedAction targettedAction)
            {
                actionResult = ProcessTargettedAction(action, source, actionResult, targettedAction);
            }
            else if (action.ActionType == ActionType.Ability && action is IAbilityAction abilityAction)
            {
                actionResult = ProcessAbilityAction(source, null, actionResult, abilityAction);
            }
            else
            {
                throw new UnknownActionTypeException(action.ActionType);
            }

            if (source.CurrentHealth < 0)
            {
                source.CurrentHealth = 0;
            }

            var fallenCharactersAfter = characters.Where(c => c.CurrentHealth <= 0);
            var newlyFallenCharacters = fallenCharactersAfter.Where(c => !fallenCharactersBefore.Contains(c));

            var actionResults = new List<ActionResult>()
            {
                actionResult with { Characters = characters.Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList() }
            };

            actionResults.AddRange(newlyFallenCharacters.Select(c => new ActionResult(combatTime, c, $"{c.Name} has fallen.", null, characters.Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList(), ImmutableList<CombatEvent>.Empty)));

            return actionResults.ToImmutableList();
        }

        private ActionResult ProcessTargettedAction(IAction action, CharacterBase source, ActionResult actionResult, ITargettedAction targettedAction)
        {
            if (targettedAction.Target is CharacterBase target)
            {
                if (action.ActionType == ActionType.Attack)
                {
                    return ProcessAttackAction(source, target, actionResult);
                }
                else if (action.ActionType == ActionType.Ability && action is IAbilityAction abilityAction)
                {
                    return ProcessAbilityAction(source, target, actionResult, abilityAction);
                }
                else
                {
                    throw new UnknownActionTypeException(action.ActionType);
                }
            }
            else
            {
                throw new InvalidTargetException($"Target must be a DungeonBot or Enemy: {targettedAction.Target}");
            }
        }

        private ActionResult ProcessAttackAction(CharacterBase source, CharacterBase target, ActionResult actionResult)
        {
            var attackDamage = _combatValueCalculator.GetAttackValue(source, target);

            target.CurrentHealth -= attackDamage;

            if (target.CurrentHealth < 0)
            {
                target.CurrentHealth = 0;
            }

            return actionResult with { DisplayText = $"{source.Name} attacked {target.Name} for {attackDamage} damage." };
        }

        private ActionResult ProcessAbilityAction(CharacterBase source, CharacterBase? target, ActionResult actionResult, IAbilityAction abilityAction)
        {
            if (!source.Abilities.ContainsKey(abilityAction.AbilityType))
            {
                throw new AbilityNotAvailableException($"{source.Name} does not have access to the ability {abilityAction.AbilityType}.");
            }

            var abilityContext = source.Abilities[abilityAction.AbilityType];

            if (!abilityContext.IsAvailable)
            {
                throw new AbilityNotAvailableException($"{source.Name} can not use {abilityAction.AbilityType} because it is not ready yet.");
            }

            string? displayText;

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

                    displayText = $"{source.Name} took a heavy swing at {target.Name} for {abilityDamage} damage.";
                    break;

                case AbilityType.LickWounds:

                    source.CurrentHealth = source.MaximumHealth;

                    displayText = $"{source.Name} licked it's wounds because a DungeonBot used an ability last turn.";
                    break;

                default:
                    throw new UnknownAbilityTypeException(abilityAction.AbilityType);
            }

            if (target?.CurrentHealth < 0)
            {
                target.CurrentHealth = 0;
            }

            if (abilityContext.CooldownCombatTime > 0)
            {
                source.Abilities[abilityAction.AbilityType] = abilityContext with
                {
                    IsAvailable = false
                };

                return actionResult with
                {
                    DisplayText = displayText,
                    NewCombatEvents = ImmutableList.Create<CombatEvent>(new CombatEvent<AbilityType>(
                        actionResult.CombatTime + source.Abilities[abilityAction.AbilityType].CooldownCombatTime,
                        source,
                        CombatEventType.CooldownReset,
                        abilityAction.AbilityType))
                };
            }

            return actionResult with {
                DisplayText = displayText
            };
        }
    }
}
