using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.Dungeons
{
    public class DungeonResultAction
    {
        public DungeonViewModel Dungeon { get; }
        public DungeonResultViewModel DungeonResult { get; }

        public DungeonResultAction(DungeonViewModel dungeon, DungeonResultViewModel dungeonResult)
        {
            Dungeon = dungeon;
            DungeonResult = dungeonResult;
        }
    }
}
