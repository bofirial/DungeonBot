using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.Adventures
{
    public record RunAdventureAction
    {
        public AdventureViewModel Adventure { get; init; }

        public ActionModuleLibraryViewModel ActionModuleLibrary { get; init; }

        public string RunId { get; }

        public RunAdventureAction(AdventureViewModel adventure, ActionModuleLibraryViewModel actionModuleLibrary, string runId)
        {
            Adventure = adventure;
            ActionModuleLibrary = actionModuleLibrary;
            RunId = runId;
        }
    }
}
