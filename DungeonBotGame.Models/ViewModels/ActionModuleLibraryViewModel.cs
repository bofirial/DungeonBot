using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;

namespace DungeonBotGame.Models.ViewModels
{
    public record ActionModuleLibraryViewModel
    {
        public ReadOnlyCollection<byte> Assembly { get; init; }

        public ReadOnlyCollection<ActionModuleFileViewModel> ActionModuleFiles { get; init; }

        public ActionModuleLibraryViewModel(byte[] assembly, params ActionModuleFileViewModel[] actionModuleFiles)
        {
            Assembly = assembly.ToList().AsReadOnly();
            ActionModuleFiles = actionModuleFiles.ToList().AsReadOnly();
        }

        [JsonConstructor]
        public ActionModuleLibraryViewModel(IEnumerable<ActionModuleFileViewModel> actionModuleFiles)
        {
            ActionModuleFiles = actionModuleFiles.ToList().AsReadOnly();
        }
    }
}
