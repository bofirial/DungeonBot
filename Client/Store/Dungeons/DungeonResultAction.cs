using DungeonBot.Models.Display;

namespace DungeonBot.Client.Store.Dungeons
{
    public class DungeonResultAction
    {
        public Dungeon Dungeon { get; }
        public DungeonResult DungeonResult { get; }

        public DungeonResultAction(Dungeon dungeon, DungeonResult dungeonResult)
        {
            Dungeon = dungeon;
            DungeonResult = dungeonResult;
        }
    }
}
