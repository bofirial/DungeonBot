using System.Collections.Generic;
using System.Collections.Immutable;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Models.Combat
{
    public class DungeonBot : CharacterBase, IDungeonBot
    {
        public DungeonBot(string dungeonBotName,
            short level,
            short power,
            short armor,
            short speed,
            IImmutableList<ActionModuleFileViewModel> sourceCodeFiles,
            ActionModuleContext actionModuleContext,
            IDictionary<AbilityType, AbilityContext> abilities,
            IList<CombatEffect> combatEffects) :
            base(dungeonBotName, level, power, armor, speed, sourceCodeFiles, abilities, combatEffects)
        {
            ActionModuleContext = actionModuleContext;
        }

        [Newtonsoft.Json.JsonIgnore]
        public ActionModuleContext ActionModuleContext { get; }
    }
}
