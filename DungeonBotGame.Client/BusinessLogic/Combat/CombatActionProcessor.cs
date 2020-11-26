﻿using System.Collections.Generic;
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

            var removeAfterActionEffectTypes = new CombatEffectType[] { CombatEffectType.StunTarget };
            var removeAfterActionEffects = source.CombatEffects.Where(c => removeAfterActionEffectTypes.Contains(c.CombatEffectType)).ToList();

            foreach (var removeAfterActionEffect in removeAfterActionEffects)
            {
                source.CombatEffects.Remove(removeAfterActionEffect);
            }

            return actionResults.ToImmutableList();
        }

        private ActionResult ProcessTargettedAction(IAction action, CharacterBase source, ActionResult actionResult, ITargettedAction targettedAction)
        {
            if (targettedAction.Target is CharacterBase target)
            {
                var targetCombatEffectTypes = new CombatEffectType[] { CombatEffectType.StunTarget };
                var targetCombatEffects = source.CombatEffects.Where(c => targetCombatEffectTypes.Contains(c.CombatEffectType)).ToList();

                foreach (var targetCombatEffect in targetCombatEffects)
                {
                    switch (targetCombatEffect.CombatEffectType)
                    {
                        case CombatEffectType.StunTarget:
                            target.CombatEffects.Add(new CombatEffect("Stunned", CombatEffectType.Stunned, targetCombatEffect.Value, CombatTime: null, CombatTimeInterval: null));

                            source.CombatEffects.Remove(targetCombatEffect);
                            break;
                    }
                }

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
            var newCombatEvents = new List<CombatEvent>();

            target.CurrentHealth -= attackDamage;

            if (target.CurrentHealth < 0)
            {
                target.CurrentHealth = 0;
            }

            var salvageStrikesCombatEffectTypes = new CombatEffectType[] { CombatEffectType.SalvageStrikes };
            var salvageStrikesCombatEffects = source.CombatEffects.Where(c => salvageStrikesCombatEffectTypes.Contains(c.CombatEffectType)).ToList();

            foreach (var salvageStrikesCombatEffect in salvageStrikesCombatEffects)
            {
                switch (salvageStrikesCombatEffect.CombatEffectType)
                {
                    case CombatEffectType.SalvageStrikes:
                        var combatEffect = new CombatEffect("Salvage Strikes", CombatEffectType.DamageOverTime, (short)(attackDamage * 0.05), CombatTime: actionResult.CombatTime + 200, CombatTimeInterval: 100);
                        target.CombatEffects.Add(combatEffect);

                        newCombatEvents.Add(new CombatEvent<CombatEffect>(actionResult.CombatTime + 100, target, CombatEventType.CombatEffect, combatEffect));

                        source.CurrentHealth += (short)(attackDamage * 0.05);
                        break;
                }
            }

            return actionResult with { DisplayText = $"{source.Name} attacked {target.Name} for {attackDamage} damage.", NewCombatEvents = newCombatEvents.ToImmutableList() };
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
            var newCombatEvents = new List<CombatEvent>();

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

                case AbilityType.AnalyzeSituation:

                    source.CombatEffects.Add(new CombatEffect("Situational Analysis - Attack Damage", CombatEffectType.AttackPercentage, Value: 200, CombatTime: null, CombatTimeInterval: null));
                    source.CombatEffects.Add(new CombatEffect("Situational Analysis - Action Time", CombatEffectType.ActionCombatTimePercentage, Value: 50, CombatTime: null, CombatTimeInterval: null));

                    displayText = $"{source.Name} performs combat analysis.";
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

                newCombatEvents.Add(new CombatEvent<AbilityType>(
                        actionResult.CombatTime + source.Abilities[abilityAction.AbilityType].CooldownCombatTime,
                        source,
                        CombatEventType.CooldownReset,
                        abilityAction.AbilityType));
            }

            return actionResult with {
                DisplayText = displayText,
                NewCombatEvents = newCombatEvents.ToImmutableList()
            };
        }
    }
}
