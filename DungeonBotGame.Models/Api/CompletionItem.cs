namespace DungeonBotGame.Models.Api
{
    public class CompletionItem
    {
        public string Label { get; set; } = string.Empty;

        public string InsertText { get; set; } = string.Empty;

        public string Kind { get; set; }

        public string Detail { get; set; } = string.Empty;
    }
}
