using System;

namespace DungeonBot.Models.Combat
{
    public class Player : ICharacter
    {
        public Player(string dungeonBotName, int maximumHealth, ActionModuleContext actionModuleContext)
        {
            Name = dungeonBotName;
            MaximumHealth = maximumHealth;
            ActionModuleContext = actionModuleContext;
            CurrentHealth = maximumHealth;
            Id = Guid.NewGuid().ToString();
        }

        public string Name { get; }

        public int CurrentHealth { get; set; }

        public int MaximumHealth { get; }

        public ActionModuleContext ActionModuleContext { get; }

        public string Id { get; }
    }
}
