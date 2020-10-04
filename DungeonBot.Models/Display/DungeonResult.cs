namespace DungeonBot.Models.Display
{
    public class DungeonResult
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
