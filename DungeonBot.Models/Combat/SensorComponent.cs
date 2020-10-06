namespace DungeonBot.Models.Combat
{
    public class SensorComponent : ISensorComponent
    {
        public SensorComponent(IPlayer dungeonBot, IEnemy enemy)
        {
            DungeonBot = dungeonBot;
            Enemy = enemy;
        }

        public IPlayer DungeonBot { get; }

        public IEnemy Enemy { get; }
    }
}
