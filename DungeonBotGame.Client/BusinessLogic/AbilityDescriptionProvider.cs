﻿using System.Collections.Generic;
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
            { AbilityType.HeavySwing, new AbilityDescriptionViewModel("Heavy Swing", "A strong strike that deals three times normal attack damage.", AbilityType.HeavySwing, IsTargettedAbility: true, CooldownCombatTime: 500) },
            { AbilityType.AnalyzeSituation, new AbilityDescriptionViewModel("Analyze Situation", "Analyzes the environment and enemies, allowing the user to deal double damage with its next action.  The user's next action requires half the normal time to prepare.", AbilityType.AnalyzeSituation, IsTargettedAbility: false, CooldownCombatTime: 2000)},
            { AbilityType.LickWounds, new AbilityDescriptionViewModel("Lick Wounds", "Fully heals the user.  Only useable after an enemy uses an ability.", AbilityType.LickWounds, IsTargettedAbility: false, CooldownCombatTime: 0) }
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

