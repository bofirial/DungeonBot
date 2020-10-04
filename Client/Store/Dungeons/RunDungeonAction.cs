using DungeonBot.Models.Display;

namespace DungeonBot.Client.Store.Dungeons
{
    public class RunDungeonAction
    {
        public Dungeon Dungeon { get; }

        public ActionModuleLibrary ActionModuleLibrary { get; }

        public string RunId { get; }

        public RunDungeonAction(Dungeon dungeon, ActionModuleLibrary actionModuleLibrary, string runId)
        {
            Dungeon = dungeon;
            ActionModuleLibrary = actionModuleLibrary;
            RunId = runId;
        }
    }
}
