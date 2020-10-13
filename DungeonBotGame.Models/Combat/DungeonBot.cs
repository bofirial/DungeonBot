using System.Collections.Generic;

namespace DungeonBotGame.Models.Combat
{
    public class DungeonBot : CharacterBase, IDungeonBot
    {
        public DungeonBot(string dungeonBotName, int maximumHealth, ActionModuleContext actionModuleContext, Dictionary<AbilityType, AbilityContext> abilities) :
            base(dungeonBotName, maximumHealth, abilities)
        {
            ActionModuleContext = actionModuleContext;
        }

        public ActionModuleContext ActionModuleContext { get; }
    }
}
