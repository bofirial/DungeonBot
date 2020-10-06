using System;

namespace DungeonBot.Models.Combat
{
    public abstract class CharacterBase : ICharacter
    {
        public CharacterBase(string characterName, int maximumHealth)
        {
            Name = characterName;
            MaximumHealth = maximumHealth;
            CurrentHealth = maximumHealth;
            Id = Guid.NewGuid().ToString();
        }

        public string Name { get; }

        public int CurrentHealth { get; set; }

        public int MaximumHealth { get; }

        public string Id { get; }
    }
}
