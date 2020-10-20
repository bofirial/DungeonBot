using System.Threading.Tasks;
using DungeonBotGame.Models.Api;
using DungeonBotGame.Server.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace DungeonBotGame.Server.Controllers
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

        public async Task<CodeCompletionPostResponseModel?> PostAsync([FromBody] CodeCompletionPostRequestModel requestModel) => await _codeCompletionBuilder.GetCodeCompletionsAsync(requestModel);
    }
}
