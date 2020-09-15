using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;

namespace DungeonBot.Models
{
    public class ActionModuleLibrary
    {
        public string Name { get; }

        public ReadOnlyCollection<byte> Assembly { get; }

        public ReadOnlyCollection<ActionModuleFile> ActionModuleFiles { get; }

        public ActionModuleLibrary(string name, byte[] assembly, params ActionModuleFile[] actionModuleFiles)
        {
            Name = name;
            Assembly = assembly.ToList().AsReadOnly();
            ActionModuleFiles = actionModuleFiles.ToList().AsReadOnly();
        }

        [JsonConstructor]
        public ActionModuleLibrary(string name, List<ActionModuleFile> actionModuleFiles)
        {
            Name = name;
            ActionModuleFiles = actionModuleFiles.AsReadOnly();
        }
    }
}
