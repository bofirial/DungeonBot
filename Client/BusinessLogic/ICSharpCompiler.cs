using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;

namespace DungeonBot.Client.BusinessLogic
{
    public interface ICSharpCompiler
    {
        Task<CSharpCompilation> CompileAsync(string code);
    }
}
