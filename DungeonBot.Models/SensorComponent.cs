namespace DungeonBot.Models
{
    public class SensorComponent : ISensorComponent
    {
        public SensorComponent(IEnemy enemy)
        {
            Enemy = enemy;
        }

        public IEnemy Enemy { get; }
    }
}
