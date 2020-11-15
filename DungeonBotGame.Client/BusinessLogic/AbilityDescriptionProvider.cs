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
            { AbilityType.HeavySwing, new AbilityDescriptionViewModel("Heavy Swing", "A strong strike that deals three times normal attack damage.", AbilityType.HeavySwing, CooldownRounds: 1, IsTargettedAbility: true, StartOfCombatCooldownRounds: 0) },
            { AbilityType.LickWounds, new AbilityDescriptionViewModel("Lick Wounds", "Fully heals the user.  Only useable after an enemy uses an ability.", AbilityType.LickWounds, CooldownRounds: 0, IsTargettedAbility: false, StartOfCombatCooldownRounds: 0) }
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

