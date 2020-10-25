using System.Threading.Tasks;
using DungeonBotGame.Models.Api;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic.Compilation
{
    public interface ICodeCompletionService
    {
        Task InitializeCodeEditorAsync(DungeonBotViewModel dungeonBotViewModel);

        Task<CodeCompletionPostResponseModel> GetCodeCompletionsAsync(string sourceCode, int currentPosition);
    }
}
