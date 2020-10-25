using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.Adventures
{
    public class RunAdventureAction
    {
        public AdventureViewModel Adventure { get; }

        public ActionModuleLibraryViewModel ActionModuleLibrary { get; }

        public string RunId { get; }

        public RunAdventureAction(AdventureViewModel adventure, ActionModuleLibraryViewModel actionModuleLibrary, string runId)
        {
            Adventure = adventure;
            ActionModuleLibrary = actionModuleLibrary;
            RunId = runId;
        }
    }
}
