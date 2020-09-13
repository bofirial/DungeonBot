using System.Threading.Tasks;
using DungeonBot.Models;

namespace DungeonBot.Server.BusinessLogic
{
    public interface ICodeCompletionBuilder
    {
        Task<CodeCompletionPostResponseModel> GetCodeCompletionsAsync(CodeCompletionPostRequestModel requestModel);
    }
}
