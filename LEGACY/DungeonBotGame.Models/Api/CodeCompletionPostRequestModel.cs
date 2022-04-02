using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Models.Api
{
    public record CodeCompletionPostRequestModel(string TargetFileName, int TargetFilePosition, DungeonBotViewModel DungeonBot);
}
