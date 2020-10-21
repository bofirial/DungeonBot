using System.Threading.Tasks;
using DungeonBotGame.Models.ViewModels;
using Microsoft.CodeAnalysis.CSharp;

namespace DungeonBotGame.Client.BusinessLogic
{
    public interface ICSharpCompiler
    {
        Task<CSharpCompilation> CompileAsync(string code, DungeonBotViewModel dungeonBot);
    }
}
