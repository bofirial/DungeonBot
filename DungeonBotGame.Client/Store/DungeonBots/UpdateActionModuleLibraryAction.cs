using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DungeonBotGame.Models.Display;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public class UpdateActionModuleLibraryAction
    {
        public string NewActionModuleLibraryName { get; }

        public string PreviousActionModuleLibraryName { get; }

        public ReadOnlyCollection<byte> Assembly { get; }

        public ReadOnlyCollection<ActionModuleFileViewModel> ActionModuleFiles { get; }

        public UpdateActionModuleLibraryAction(string newActionModuleLibraryName, string previousActionModuleLibraryName, IEnumerable<byte> assembly, IEnumerable<ActionModuleFileViewModel> actionModuleFiles)
        {
            NewActionModuleLibraryName = newActionModuleLibraryName;
            PreviousActionModuleLibraryName = previousActionModuleLibraryName;
            Assembly = assembly.ToList().AsReadOnly();
            ActionModuleFiles = actionModuleFiles.ToList().AsReadOnly();
        }
    }
}
