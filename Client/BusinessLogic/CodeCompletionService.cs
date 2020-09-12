using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace DungeonBot.Client.BusinessLogic
{
    public class CodeCompletionService : ICodeCompletionService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly ILogger<CodeCompletionService> _logger;

        public CodeCompletionService(IJSRuntime jsRuntime, ILogger<CodeCompletionService> logger)
        {
            _jsRuntime = jsRuntime;
            _logger = logger;
        }

        public async Task InitializeCodeEditorAsync()
        {
            await _jsRuntime.InvokeVoidAsync("initializeMonacoCodeEditor", DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public async Task<string> GetCodeCompletionsAsync(string sourceCode, int currentPosition)
        {
            _logger.LogInformation(sourceCode);

            return "It works!";
        }
    }
}
