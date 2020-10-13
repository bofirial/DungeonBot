using System.Threading.Tasks;
using DungeonBotGame.Models.Api;

namespace DungeonBotGame.Server.BusinessLogic
{
    public interface ICodeCompletionBuilder
    {
        Task<CodeCompletionPostResponseModel> GetCodeCompletionsAsync(CodeCompletionPostRequestModel requestModel);
    }
}
