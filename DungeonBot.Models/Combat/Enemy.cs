using System;

namespace DungeonBot.Models.Combat
{
    public class Enemy : IEnemy
    {
        public Enemy(string enemyName, int maximumHealth)
        {
            Name = enemyName;
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
