using System.Collections.Generic;

namespace DungeonBotGame.Models.ViewModels
{
    public class DungeonBotViewModel
    {
        public string Id { get; }

        public string Name { get; }

        public string ProfileImageLocation { get; }

        public ActionModuleLibraryViewModel ActionModuleLibrary { get; }

        public IReadOnlyCollection<AbilityType> Abilities { get; }

        public DungeonBotViewModel(string id, string name, string profileImageLocation, ActionModuleLibraryViewModel actionModuleLibrary, List<AbilityType> abilities)
        {
            Id = id;
            Name = name;
            ProfileImageLocation = profileImageLocation;
            ActionModuleLibrary = actionModuleLibrary;
            Abilities = abilities.AsReadOnly();
        }
    }
}
