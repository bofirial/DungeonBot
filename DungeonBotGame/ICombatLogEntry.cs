namespace DungeonBotGame
{
    public interface ICombatLogEntry
    {
        public int CombatTime { get; }

        public string DisplayText { get; }

        public ICharacter Character { get; }

        public IAction Action { get; }
    }
}
