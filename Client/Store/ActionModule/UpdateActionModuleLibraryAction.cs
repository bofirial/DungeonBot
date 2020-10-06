using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DungeonBot.Models.Display;

namespace DungeonBot.Client.Store.ActionModule
{
    public class UpdateActionModuleLibraryAction
    {
        public string NewActionModuleLibraryName { get; }

        public string PreviousActionModuleLibraryName { get; }

        public ReadOnlyCollection<byte> Assembly { get; }

        public ReadOnlyCollection<ActionModuleFile> ActionModuleFiles { get; }

        public UpdateActionModuleLibraryAction(string newActionModuleLibraryName, string previousActionModuleLibraryName, IEnumerable<byte> assembly, IEnumerable<ActionModuleFile> actionModuleFiles)
        {
            NewActionModuleLibraryName = newActionModuleLibraryName;
            PreviousActionModuleLibraryName = previousActionModuleLibraryName;
            Assembly = assembly.ToList().AsReadOnly();
            ActionModuleFiles = actionModuleFiles.ToList().AsReadOnly();
        }
    }
}
