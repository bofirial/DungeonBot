using System.Collections.Immutable;

namespace DungeonBotGame.Models.Api
{
    public record CodeCompletionPostResponseModel(ImmutableList<CompletionItem> CompletionItems, string ErrorMessage);
}
