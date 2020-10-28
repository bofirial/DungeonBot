using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public record UpdateActionModuleLibraryAction
    {
        public string NewActionModuleLibraryName { get; init; }

        public string PreviousActionModuleLibraryName { get; init; }

        public ReadOnlyCollection<byte> Assembly { get; init; }

        public ReadOnlyCollection<ActionModuleFileViewModel> ActionModuleFiles { get; init; }

        public UpdateActionModuleLibraryAction(string newActionModuleLibraryName, string previousActionModuleLibraryName, IEnumerable<byte> assembly, IEnumerable<ActionModuleFileViewModel> actionModuleFiles)
        {
            NewActionModuleLibraryName = newActionModuleLibraryName;
            PreviousActionModuleLibraryName = previousActionModuleLibraryName;
            Assembly = assembly.ToList().AsReadOnly();
            ActionModuleFiles = actionModuleFiles.ToList().AsReadOnly();
        }
    }
}
