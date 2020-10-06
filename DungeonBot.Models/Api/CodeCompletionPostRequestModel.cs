using DungeonBot.Models.Display;

namespace DungeonBot.Models.Api
{
    public class CodeCompletionPostRequestModel
    {
        public ActionModuleLibrary ActionModuleLibrary { get; set; }

        public string TargetFileName { get; set; } = string.Empty;

        public int TargetFilePosition { get; set; }
    }
}
