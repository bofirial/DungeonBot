using System.Threading.Tasks;
using DungeonBotGame.Client.Store.Dungeons;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic
{
    public interface IDungeonRunner
    {
        Task<DungeonResultViewModel> RunDungeonAsync(RunDungeonAction runDungeonAction);
    }
}
