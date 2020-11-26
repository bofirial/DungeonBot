using System.Collections.Immutable;
using System.Linq;
using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface ICombatActionProcessor
    {
        void ProcessAction(IAction action, CharacterBase source, CombatContext combatContext);
    }

    public class CombatActionProcessor : ICombatActionProcessor
    {
        private readonly ICombatValueCalculator _combatValueCalculator;

        public CombatActionProcessor(ICombatValueCalculator combatValueCalculator)
        {
            _combatValueCalculator = combatValueCalculator;
        }

        public void ProcessAction(IAction action, CharacterBase source, CombatContext combatContext)
        {
            var fallenCharactersBefore = combatContext.Characters.Where(c => c.CurrentHealth <= 0).ToList();

            if (action is ITargettedAction targettedAction)
            {
                ProcessTargettedAction(targettedAction, source, combatContext);
            }
            else if (action.ActionType == ActionType.Ability && action is IAbilityAction abilityAction)
            {
                ProcessAbilityAction(abilityAction, source, null, combatContext);
            }
            else
            {
                throw new UnknownActionTypeException(action.ActionType);
            }

            if (source.CurrentHealth < 0)
            {
                source.CurrentHealth = 0;
            }

            var fallenCharactersAfter = combatContext.Characters.Where(c => c.CurrentHealth <= 0);
            var newlyFallenCharacters = fallenCharactersAfter.Where(c => !fallenCharactersBefore.Contains(c));

            combatContext.CombatLog.AddRange(newlyFallenCharacters.Select(c => new CombatLogEntry(combatContext.CombatTimer, c, $"{c.Name} has fallen.", null, combatContext.Characters.Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList())));

            var removeAfterActionEffectTypes = new CombatEffectType[] { CombatEffectType.StunTarget };
            var removeAfterActionEffects = source.CombatEffects.Where(c => removeAfterActionEffectTypes.Contains(c.CombatEffectType)).ToList();

            foreach (var removeAfterActionEffect in removeAfterActionEffects)
            {
                source.CombatEffects.Remove(removeAfterActionEffect);
            }
        }

        private void ProcessTargettedAction(ITargettedAction targettedAction, CharacterBase source, CombatContext combatContext)
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

                if (targettedAction.ActionType == ActionType.Attack)
                {
                    ProcessAttackAction(targettedAction, source, target, combatContext);
                }
                else if (targettedAction.ActionType == ActionType.Ability && targettedAction is IAbilityAction abilityAction)
                {
                    ProcessAbilityAction(abilityAction, source, target, combatContext);
                }
                else
                {
                    throw new UnknownActionTypeException(targettedAction.ActionType);
                }
            }
            else
            {
                throw new InvalidTargetException($"Target must be a DungeonBot or Enemy: {targettedAction.Target}");
            }
        }

        private void ProcessAttackAction(IAction action, CharacterBase source, CharacterBase target, CombatContext combatContext)
        {
            var attackDamage = _combatValueCalculator.GetAttackValue(source, target);

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
                        var combatEffect = new CombatEffect("Salvage Strikes", CombatEffectType.DamageOverTime, (short)(attackDamage * 0.05), CombatTime: combatContext.CombatTimer + 200, CombatTimeInterval: 100);
                        target.CombatEffects.Add(combatEffect);

                        combatContext.NewCombatEvents.Add(new CombatEvent<CombatEffect>(combatContext.CombatTimer + 100, target, CombatEventType.CombatEffect, combatEffect));

                        source.CurrentHealth += (short)(attackDamage * 0.05);
                        break;
                }
            }

            combatContext.CombatLog.Add(new CombatLogEntry(combatContext.CombatTimer, source, $"{source.Name} attacked {target.Name} for {attackDamage} damage.", action, combatContext.Characters.Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList()));
        }

        private void ProcessAbilityAction(IAbilityAction abilityAction, CharacterBase source, CharacterBase? target, CombatContext combatContext)
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

                combatContext.NewCombatEvents.Add(new CombatEvent<AbilityType>(
                        combatContext.CombatTimer + source.Abilities[abilityAction.AbilityType].CooldownCombatTime,
                        source,
                        CombatEventType.CooldownReset,
                        abilityAction.AbilityType));
            }

            combatContext.CombatLog.Add(new CombatLogEntry(combatContext.CombatTimer, source, displayText, abilityAction, combatContext.Characters.Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList()));
        }
    }
}
