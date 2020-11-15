using System.Collections.Generic;
using DungeonBotGame.Models.Combat;
using Newtonsoft.Json;

namespace DungeonBotGame.Models.ViewModels
{
    public record DungeonBotViewModel
    {
        public DungeonBotViewModel(
            string id,
            string name,
            string profileImageLocation,
            IReadOnlyCollection<ActionModuleFileViewModel> actionModuleFiles,
            IReadOnlyCollection<AbilityType> abilities,
            ActionModuleContext actionModuleContext,
            IReadOnlyCollection<ErrorViewModel> errors)
        {
            Id = id;
            Name = name;
            ProfileImageLocation = profileImageLocation;
            ActionModuleFiles = actionModuleFiles;
            Abilities = abilities;
            ActionModuleContext = actionModuleContext;
            Errors = errors;
        }

        [JsonConstructor]
        public DungeonBotViewModel(
            string id,
            string name,
            string profileImageLocation,
            IReadOnlyCollection<ActionModuleFileViewModel> actionModuleFiles,
            IReadOnlyCollection<AbilityType> abilities,
            IReadOnlyCollection<ErrorViewModel> errors)
        {
            Id = id;
            Name = name;
            ProfileImageLocation = profileImageLocation;
            ActionModuleFiles = actionModuleFiles;
            Abilities = abilities;
            Errors = errors;
        }

        public string Id { get; init; }
        public string Name { get; init; }
        public string ProfileImageLocation { get; init; }
        public IReadOnlyCollection<ActionModuleFileViewModel> ActionModuleFiles { get; init; }
        public IReadOnlyCollection<AbilityType> Abilities { get; init; }
        public IReadOnlyCollection<ErrorViewModel> Errors { get; init; }

        [JsonIgnore]
        public ActionModuleContext ActionModuleContext { get; init; }
    }
}
