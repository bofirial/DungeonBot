using System.Collections.Generic;

namespace DungeonBotGame.Models.ViewModels
{
    public record DungeonBotViewModel
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string ProfileImageLocation { get; init; }

        public ActionModuleLibraryViewModel ActionModuleLibrary { get; init; }

        public IReadOnlyCollection<AbilityType> Abilities { get; init; }

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
