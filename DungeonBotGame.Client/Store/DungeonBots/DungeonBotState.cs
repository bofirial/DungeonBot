using System.Collections.Generic;
using System.Linq;
using DungeonBotGame.Models.Display;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public class DungeonBotState
    {
        public IReadOnlyCollection<ActionModuleLibrary> ActionModuleLibraries { get; }

        public DungeonBotState(IEnumerable<ActionModuleLibrary> actionModuleLibraries)
        {
            ActionModuleLibraries = actionModuleLibraries.ToList().AsReadOnly();
        }
    }
}
