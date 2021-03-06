﻿using System.Collections.Generic;
using System.Linq;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface IAbilityContextDictionaryBuilder
    {
        IDictionary<AbilityType, AbilityContext> BuildAbilityContextDictionary(IEnumerable<AbilityType> abilities);
    }

    public class AbilityContextDictionaryBuilder : IAbilityContextDictionaryBuilder
    {
        private readonly IAbilityDescriptionProvider _abilityDescriptionProvider;

        public AbilityContextDictionaryBuilder(IAbilityDescriptionProvider abilityDescriptionProvider)
        {
            _abilityDescriptionProvider = abilityDescriptionProvider;
        }

        public IDictionary<AbilityType, AbilityContext> BuildAbilityContextDictionary(IEnumerable<AbilityType> abilities)
        {
            return abilities.ToDictionary(
                abilityType => abilityType,
                abilityType =>
                {
                    var abilityDescription = _abilityDescriptionProvider.GetAbilityDescription(abilityType);

                    return new AbilityContext(IsAvailable: true, abilityDescription.CooldownCombatTime);
                });
        }
    }
}
