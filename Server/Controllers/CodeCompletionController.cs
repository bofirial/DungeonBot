using System.Threading.Tasks;
using DungeonBot.Models;
using DungeonBot.Server.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace DungeonBot.Server.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class CodeCompletionController : ControllerBase
    {
        private readonly ICodeCompletionBuilder _codeCompletionBuilder;

        public CodeCompletionController(ICodeCompletionBuilder codeCompletionBuilder)
        {
            _codeCompletionBuilder = codeCompletionBuilder;
        }

        public async Task<CodeCompletionPostResponseModel?> PostAsync([FromBody] CodeCompletionPostRequestModel requestModel)
        {
            return await _codeCompletionBuilder.GetCodeCompletionsAsync(requestModel);
        }
    }
}
