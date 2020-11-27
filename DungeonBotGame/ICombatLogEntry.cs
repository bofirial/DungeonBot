namespace DungeonBotGame
{
    public interface ICombatLogEntry
    {
        public int CombatTime { get; }

        public string DisplayText { get; }

        public ICharacter Character { get; }
    }

    public interface ICombatLogEntry<TLogData> : ICombatLogEntry
    {
        public TLogData LogData { get; }
    }
}
