using System.Threading.Tasks;

namespace DungeonBot.Client.BusinessLogic
{
    public interface ICodeCompletionService
    {
        Task InitializeCodeEditorAsync();

        Task<string> GetCodeCompletionsAsync(string sourceCode, int currentPosition);
    }
}
