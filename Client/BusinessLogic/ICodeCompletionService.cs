using System.Threading.Tasks;
using DungeonBot.Models;

namespace DungeonBot.Client.BusinessLogic
{
    public interface ICodeCompletionService
    {
        Task InitializeCodeEditorAsync();

        Task<CodeCompletionPostResponseModel> GetCodeCompletionsAsync(string sourceCode, int currentPosition);
    }
}
