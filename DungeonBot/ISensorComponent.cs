namespace DungeonBot
{
    public interface ISensorComponent
    {
        public IPlayer DungeonBot { get; }

        public IEnemy Enemy { get; }
    }
}
