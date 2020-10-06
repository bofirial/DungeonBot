namespace DungeonBot.Models.Display
{
    public class EncounterResult : ICombatResult
    {
        public bool Success { get; }

        public EncounterResult(bool success)
        {
            Success = success;
        }
    }
}
