using System.Threading.Tasks;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.Models.Display;

namespace DungeonBotGame.Client.BusinessLogic
{
    public interface IActionModuleContextProvider
    {
        Task<ActionModuleContext> GetActionModuleContext(ActionModuleLibrary actionModuleLibrary);
    }
}
