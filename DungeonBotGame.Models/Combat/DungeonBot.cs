using System.Collections.Generic;

namespace DungeonBotGame.Models.Combat
{
    public class DungeonBot : CharacterBase, IDungeonBot
    {
        public DungeonBot(string dungeonBotName, int maximumHealth, string sourceCode, ActionModuleContext actionModuleContext, Dictionary<AbilityType, AbilityContext> abilities) :
            base(dungeonBotName, maximumHealth, sourceCode, abilities)
        {
            ActionModuleContext = actionModuleContext;
        }

        [Newtonsoft.Json.JsonIgnore]
        public ActionModuleContext ActionModuleContext { get; }
    }
}
