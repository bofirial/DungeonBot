using System.Collections.Generic;

namespace DungeonBot.Models.Api
{
    public class CodeCompletionPostResponseModel
    {
        public string ErrorMessage { get; set; } = string.Empty;

        public List<CompletionItem> CompletionItems { get; set; } = new List<CompletionItem>();
    }
}
