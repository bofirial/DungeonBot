using System.Threading.Tasks;
using DungeonBot.Models.Api;

namespace DungeonBot.Server.BusinessLogic
{
    public interface ICodeCompletionBuilder
    {
        Task<CodeCompletionPostResponseModel> GetCodeCompletionsAsync(CodeCompletionPostRequestModel requestModel);
    }
}
