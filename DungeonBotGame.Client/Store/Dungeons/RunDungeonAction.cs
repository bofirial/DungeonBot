using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.Dungeons
{
    public class RunDungeonAction
    {
        public DungeonViewModel Dungeon { get; }

        public ActionModuleLibraryViewModel ActionModuleLibrary { get; }

        public string RunId { get; }

        public RunDungeonAction(DungeonViewModel dungeon, ActionModuleLibraryViewModel actionModuleLibrary, string runId)
        {
            Dungeon = dungeon;
            ActionModuleLibrary = actionModuleLibrary;
            RunId = runId;
        }
    }
}
