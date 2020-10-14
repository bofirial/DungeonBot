using System.Collections.Generic;

namespace DungeonBotGame.Models.ViewModels
{
    public class DungeonBotViewModel
    {
        public string Id { get; }

        public string Name { get; }

        public string ProfileImageLocation { get; }

        public ActionModuleLibraryViewModel ActionModuleLibrary { get; }

        public IReadOnlyCollection<string> Abilities { get; }
    }
}
