namespace DungeonBot.Models.Display
{
    public class DungeonResult : ICombatResult
    {
        public string RunId { get; }

        public bool Success { get; }

        public DungeonResult(string runId, bool success)
        {
            RunId = runId;
            Success = success;
        }
    }
}
