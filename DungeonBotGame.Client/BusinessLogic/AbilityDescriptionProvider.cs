using System.Collections.Generic;
using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic
{
    public interface IAbilityDescriptionProvider
    {
        AbilityDescriptionViewModel GetAbilityDescription(AbilityType abilityType);
    }

    public class AbilityDescriptionProvider : IAbilityDescriptionProvider
    {
        private readonly Dictionary<AbilityType, AbilityDescriptionViewModel> _abilityDescriptions = new()
        {
            {
                AbilityType.HeavySwing,
                new AbilityDescriptionViewModel("Heavy Swing",
                    "A strong strike that deals three times normal attack damage.",
                    AbilityType.HeavySwing,
                    IsTargetted: true,
                    IsPassive: false,
                    CooldownCombatTime: 500)
            },
            {
                AbilityType.AnalyzeSituation,
                new AbilityDescriptionViewModel("Analyze Situation",
                    "Analyzes the environment and enemies, allowing the user to deal double damage with its next action.  The user's next action requires half the normal time to prepare.",
                    AbilityType.AnalyzeSituation,
                    IsTargetted: false,
                    IsPassive: false,
                    CooldownCombatTime: 2000)
            },
            {
                AbilityType.SurpriseAttack,
                new AbilityDescriptionViewModel("Surprise Attack",
                    "The first action in every encounter takes place immediately and deals double damage.  If the action targets an enemy, the enemy is stunned for 2 seconds.",
                    AbilityType.SurpriseAttack,
                    IsTargetted: false,
                    IsPassive: true,
                    CooldownCombatTime: 0)
            },
            {
                AbilityType.SalvageStrikes,
                new AbilityDescriptionViewModel("Salvage Strikes",
                    "Deal an additional 10% damage over 2 seconds whenever you damage an enemy and heal for 5% of the damage dealt.",
                    AbilityType.SalvageStrikes,
                    IsTargetted: false,
                    IsPassive: true,
                    CooldownCombatTime: 0)
            },

            {
                AbilityType.Repair,
                new AbilityDescriptionViewModel("Repair",
                    "Repairs the target using Mystic Power.",
                    AbilityType.Repair,
                    IsTargetted: true,
                    IsPassive: false,
                    CooldownCombatTime: 0)
            },

            {
                AbilityType.LickWounds,
                new AbilityDescriptionViewModel("Lick Wounds",
                    "Fully heals the user.  Only useable after an enemy uses an ability.",
                    AbilityType.LickWounds,
                    IsTargetted: false,
                    IsPassive: false,
                    CooldownCombatTime: 0)
            }
        };

        public AbilityDescriptionViewModel GetAbilityDescription(AbilityType abilityType)
        {
            if (!_abilityDescriptions.ContainsKey(abilityType))
            {
                throw new UnknownAbilityTypeException(abilityType);
            }

            return _abilityDescriptions[abilityType];
        }
    }
}

