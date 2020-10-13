using System.Threading.Tasks;
using DungeonBotGame.Models.Api;

namespace DungeonBotGame.Client.BusinessLogic
{
    public interface ICodeCompletionService
    {
        Task InitializeCodeEditorAsync();

        Task<CodeCompletionPostResponseModel> GetCodeCompletionsAsync(string sourceCode, int currentPosition);
    }
}
