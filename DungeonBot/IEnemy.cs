namespace DungeonBot
{
    public interface IEnemy : ITarget
    {
        public string Name { get; }

        public int CurrentHealth { get; }

        public int MaximumHealth { get; }
    }
}
