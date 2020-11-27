using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public class AbilityActionProcessor : IActionProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;
        private readonly ICombatValueCalculator _combatValueCalculator;

        public AbilityActionProcessor(ICombatLogEntryBuilder combatLogEntryBuilder, ICombatValueCalculator combatValueCalculator)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
            _combatValueCalculator = combatValueCalculator;
        }

        public ActionType ActionType => ActionType.Ability;

        public void ProcessAction(IAction action, CharacterBase character, CombatContext combatContext)
        {
            if (action is IAbilityAction abilityAction)
            {
                if (!character.Abilities.ContainsKey(abilityAction.AbilityType))
                {
                    throw new AbilityNotAvailableException($"{character.Name} does not have access to the ability {abilityAction.AbilityType}.");
                }

                var abilityContext = character.Abilities[abilityAction.AbilityType];

                if (!abilityContext.IsAvailable)
                {
                    throw new AbilityNotAvailableException($"{character.Name} can not use {abilityAction.AbilityType} because it is not ready yet.");
                }

                if (abilityContext.CooldownCombatTime > 0)
                {
                    SetAbilityCooldown(abilityAction.AbilityType, character, combatContext, abilityContext);
                }

                //TODO: Strategy Pattern for AbilityTypes?
                switch (abilityAction.AbilityType)
                {
                    case AbilityType.HeavySwing:

                        if (abilityAction is ITargettedAbilityAction targettedAbilityAction)
                        {
                            if (targettedAbilityAction.Target is CharacterBase target)
                            {
                                var abilityDamage = _combatValueCalculator.GetAttackValue(character, target) * 3;

                                target.CurrentHealth -= abilityDamage;

                                if (target.CurrentHealth < 0)
                                {
                                    target.CurrentHealth = 0;
                                }

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

                        break;

                    case AbilityType.AnalyzeSituation:

                        character.CombatEffects.Add(new CombatEffect("Situational Analysis - Attack Damage", CombatEffectType.AttackPercentage, Value: 200, CombatTime: null, CombatTimeInterval: null));
                        character.CombatEffects.Add(new CombatEffect("Situational Analysis - Action Time", CombatEffectType.ActionCombatTimePercentage, Value: 50, CombatTime: null, CombatTimeInterval: null));

                        combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry<IAction>($"{character.Name} performs combat analysis.", character, combatContext, abilityAction));
                        break;

                    case AbilityType.LickWounds:

                        character.CurrentHealth = character.MaximumHealth;

                        combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry<IAction>($"{character.Name} licked it's wounds because a DungeonBot used an ability last turn.", character, combatContext, abilityAction));
                        break;

                    default:
                        throw new UnknownAbilityTypeException(abilityAction.AbilityType);
                }
            }
        }

        private static void SetAbilityCooldown(AbilityType abilityType, CharacterBase source, CombatContext combatContext, AbilityContext abilityContext)
        {
            source.Abilities[abilityType] = abilityContext with
            {
                IsAvailable = false
            };

            combatContext.NewCombatEvents.Add(new CombatEvent<AbilityType>(
                    combatContext.CombatTimer + source.Abilities[abilityType].CooldownCombatTime,
                    source,
                    CombatEventType.CooldownReset,
                    abilityType));
        }
    }
}
