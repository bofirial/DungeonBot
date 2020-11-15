using System.Collections.Generic;
using System.Collections.Immutable;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Models.Combat
{
    public class DungeonBot : CharacterBase, IDungeonBot
    {
        public DungeonBot(string dungeonBotName, int maximumHealth, IImmutableList<ActionModuleFileViewModel> sourceCodeFiles, ActionModuleContext actionModuleContext, IDictionary<AbilityType, AbilityContext> abilities) :
            base(dungeonBotName, maximumHealth, sourceCodeFiles, abilities)
        {
            ActionModuleContext = actionModuleContext;
        }

        [Newtonsoft.Json.JsonIgnore]
        public ActionModuleContext ActionModuleContext { get; }
    }
}
