using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;

namespace DungeonBotGame.Models.ViewModels
{
    public class ActionModuleLibraryViewModel
    {
        public string Name { get; }

        public ReadOnlyCollection<byte> Assembly { get; }

        public ReadOnlyCollection<ActionModuleFileViewModel> ActionModuleFiles { get; }

        public ActionModuleLibraryViewModel(string name, byte[] assembly, params ActionModuleFileViewModel[] actionModuleFiles)
        {
            Name = name;
            Assembly = assembly.ToList().AsReadOnly();
            ActionModuleFiles = actionModuleFiles.ToList().AsReadOnly();
        }

        [JsonConstructor]
        public ActionModuleLibraryViewModel(string name, List<ActionModuleFileViewModel> actionModuleFiles)
        {
            Name = name;
            ActionModuleFiles = actionModuleFiles.AsReadOnly();
        }
    }
}
