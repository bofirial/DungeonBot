using System.Collections.Generic;
using System.Linq;
using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public class AbilityActionProcessor : IActionProcessor
    {
        private readonly IDictionary<AbilityType, IAbilityProcessor> _abilityProcessors;

        public AbilityActionProcessor(IEnumerable<IAbilityProcessor> abilityProcessors)
        {
            _abilityProcessors = abilityProcessors.ToDictionary(p => p.AbilityType, p => p);
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

                SetAbilityCooldown(abilityAction.AbilityType, character, combatContext, abilityContext);

                if (_abilityProcessors.ContainsKey(abilityAction.AbilityType))
                {
                    _abilityProcessors[abilityAction.AbilityType].ProcessAction(abilityAction, character, combatContext);
                }
                else
                {
                    throw new UnknownAbilityTypeException(abilityAction.AbilityType);
                }
            }
        }

        private static void SetAbilityCooldown(AbilityType abilityType, CharacterBase source, CombatContext combatContext, AbilityContext abilityContext)
        {
            if (abilityContext.CooldownCombatTime > 0)
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
}
