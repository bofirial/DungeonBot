using System.Collections.Generic;
using System.Linq;
using DungeonBot.Models;

namespace DungeonBot.Client.Store.ActionModule
{
    public class ActionModuleState
    {
        public IReadOnlyCollection<ActionModuleLibrary> ActionModuleLibraries { get; }

        public ActionModuleState(IEnumerable<ActionModuleLibrary> actionModuleLibraries)
        {
            ActionModuleLibraries = actionModuleLibraries.ToList().AsReadOnly();
        }
    }
}
