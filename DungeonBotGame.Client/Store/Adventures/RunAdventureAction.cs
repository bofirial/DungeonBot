using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.Adventures
{
    public record RunAdventureAction
    {
        public AdventureViewModel Adventure { get; init; }

        public DungeonBotViewModel DungeonBot { get; init; }

        public string RunId { get; }

        public RunAdventureAction(AdventureViewModel adventure, DungeonBotViewModel actionModuleLibrary, string runId)
        {
            Adventure = adventure;
            DungeonBot = actionModuleLibrary;
            RunId = runId;
        }
    }
}
