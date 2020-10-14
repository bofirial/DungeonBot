using System.Collections.Generic;
using System.Linq;
using DungeonBotGame.Models.Display;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public class DungeonBotState
    {
        public IReadOnlyCollection<ActionModuleLibraryViewModel> ActionModuleLibraries { get; }

        public DungeonBotState(IEnumerable<ActionModuleLibraryViewModel> actionModuleLibraries)
        {
            ActionModuleLibraries = actionModuleLibraries.ToList().AsReadOnly();
        }
    }
}
