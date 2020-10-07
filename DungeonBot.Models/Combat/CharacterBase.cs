using System;
using System.Collections.Generic;

namespace DungeonBot.Models.Combat
{
    public abstract class CharacterBase : ICharacter
    {
        public CharacterBase(string characterName, int maximumHealth, Dictionary<AbilityType, AbilityContext> abilities)
        {
            Name = characterName;
            MaximumHealth = maximumHealth;
            CurrentHealth = maximumHealth;
            Id = Guid.NewGuid().ToString();
            Abilities = abilities;
        }

        public string Name { get; }

        public int CurrentHealth { get; set; }

        public int MaximumHealth { get; }

        public Dictionary<AbilityType, AbilityContext> Abilities {get; set;}

        public string Id { get; }
    }
}
