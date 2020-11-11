using System.Collections.Generic;

namespace DungeonBotGame.Models.ViewModels
{
    public record DungeonBotViewModel(string Id, string Name, string ProfileImageLocation, ActionModuleLibraryViewModel ActionModuleLibrary, IReadOnlyCollection<AbilityType> Abilities);
}
