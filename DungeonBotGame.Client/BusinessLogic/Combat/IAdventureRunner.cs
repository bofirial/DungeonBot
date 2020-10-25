using System.Threading.Tasks;
using DungeonBotGame.Client.Store.Adventures;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface IAdventureRunner
    {
        Task<AdventureResultViewModel> RunAdventureAsync(RunAdventureAction runAdventureAction);
    }
}
