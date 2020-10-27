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
        private readonly Dictionary<AbilityType, AbilityDescriptionViewModel> _abilityDescriptions = new Dictionary<AbilityType, AbilityDescriptionViewModel>()
        {
            { AbilityType.HeavySwing, new AbilityDescriptionViewModel("Heavy Swing", "A strong strike that deals three times normal attack damage.", AbilityType.HeavySwing, cooldownRounds: 1, isTargettedAbility: true, startOfCombatCooldownRounds: 0) },
            { AbilityType.LickWounds, new AbilityDescriptionViewModel("Lick Wounds", "Fully heals the user.  Only useable after an enemy uses an ability.", AbilityType.LickWounds, cooldownRounds: 0, isTargettedAbility: false, startOfCombatCooldownRounds: 0) }
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

