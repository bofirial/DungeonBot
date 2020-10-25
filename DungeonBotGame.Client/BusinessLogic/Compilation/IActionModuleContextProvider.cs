using System.Threading.Tasks;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic.Compilation
{
    public interface IActionModuleContextProvider
    {
        Task<ActionModuleContext> GetActionModuleContext(ActionModuleLibraryViewModel actionModuleLibrary);
    }
}
