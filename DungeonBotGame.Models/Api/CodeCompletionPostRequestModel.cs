using DungeonBotGame.Models.Display;

namespace DungeonBotGame.Models.Api
{
    public class CodeCompletionPostRequestModel
    {
        public ActionModuleLibraryViewModel ActionModuleLibrary { get; set; }

        public string TargetFileName { get; set; } = string.Empty;

        public int TargetFilePosition { get; set; }
    }
}
