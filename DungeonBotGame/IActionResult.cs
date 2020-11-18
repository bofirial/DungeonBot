namespace DungeonBotGame
{
    public interface IActionResult
    {
        public int CombatTime { get; }

        public string DisplayText { get; }

        public ICharacter Character { get; }

        public IAction Action { get; }
    }
}
