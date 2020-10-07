namespace DungeonBot
{
    public interface IActionResult
    {
        public string DisplayText { get; }

        public IAction Action { get; }
    }
}
