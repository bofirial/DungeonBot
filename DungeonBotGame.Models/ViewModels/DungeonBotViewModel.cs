using System.Collections.Immutable;
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
            IImmutableList<ActionModuleFileViewModel> actionModuleFiles,
            IImmutableList<AbilityType> abilities,
            ActionModuleContext actionModuleContext,
            IImmutableList<ErrorViewModel> errors)
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
            IImmutableList<ActionModuleFileViewModel> actionModuleFiles,
            IImmutableList<AbilityType> abilities,
            IImmutableList<ErrorViewModel> errors)
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
        public IImmutableList<ActionModuleFileViewModel> ActionModuleFiles { get; init; }
        public IImmutableList<AbilityType> Abilities { get; init; }
        public IImmutableList<ErrorViewModel> Errors { get; init; }

        [JsonIgnore]
        public ActionModuleContext ActionModuleContext { get; init; }
    }
}
