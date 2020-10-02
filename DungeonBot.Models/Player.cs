using System;

namespace DungeonBot.Models
{
    public class Player : ICharacter
    {
        public Player(string dungeonBotName, int maximumHealth)
        {
            Name = dungeonBotName;
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
