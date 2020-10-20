using System.Collections.Generic;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic
{
    public class AbilityDescriptionProvider : IAbilityDescriptionProvider
    {
        private readonly Dictionary<AbilityType, AbilityDescriptionViewModel> _abilityDescriptions = new Dictionary<AbilityType, AbilityDescriptionViewModel>()
        {
            { AbilityType.HeavySwing, new AbilityDescriptionViewModel("Heavy Swing", "A strong strike that deals three times normal attack damage.", AbilityType.HeavySwing, 1) },
            { AbilityType.LickWounds, new AbilityDescriptionViewModel("Lick Wounds", "Fully heals the user.  Only useable after an enemy uses an ability.", AbilityType.LickWounds, 0) }
        };

        public AbilityDescriptionViewModel GetAbilityDescription(AbilityType abilityType)
        {
            if (!_abilityDescriptions.ContainsKey(abilityType))
            {
                //TODO: Specific Exception Types
                throw new System.Exception($"Unknown Ability Type: {abilityType}");
            }

            return _abilityDescriptions[abilityType];
        }
    }
}
