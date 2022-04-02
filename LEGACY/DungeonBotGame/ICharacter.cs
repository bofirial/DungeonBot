namespace DungeonBotGame
{
    public interface ICharacter : ITarget
    {
        public string Name { get; }

        public int CurrentHealth { get; }

        public int MaximumHealth { get; }
    }
}
