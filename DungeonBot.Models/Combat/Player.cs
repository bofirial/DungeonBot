using System.Collections.Generic;

namespace DungeonBot.Models.Combat
{
    public class Player : CharacterBase, IPlayer
    {
        public Player(string dungeonBotName, int maximumHealth, ActionModuleContext actionModuleContext, Dictionary<AbilityType, AbilityContext> abilities) :
            base(dungeonBotName, maximumHealth, abilities)
        {
            ActionModuleContext = actionModuleContext;
        }

        public ActionModuleContext ActionModuleContext { get; }
    }
}
