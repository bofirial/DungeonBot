namespace DungeonBot.Models
{
    public class CodeCompletionPostRequestModel
    {
        public CombatLogic CombatLogic { get; set; } = new CombatLogic();

        public string TargetFileName { get; set; } = string.Empty;

        public int TargetFilePosition { get; set; }
    }
}
