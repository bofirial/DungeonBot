using System.Threading.Tasks;
using DungeonBot.Models.Combat;
using DungeonBot.Models.Display;

namespace DungeonBot.Client.BusinessLogic
{
    public interface IActionModuleContextProvider
    {
        Task<ActionModuleContext> GetActionModuleContext(ActionModuleLibrary actionModuleLibrary);
    }
}
