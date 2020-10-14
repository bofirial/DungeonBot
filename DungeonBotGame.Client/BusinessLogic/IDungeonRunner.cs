using System.Threading.Tasks;
using DungeonBotGame.Client.Store.Dungeons;
using DungeonBotGame.Models.Display;

namespace DungeonBotGame.Client.BusinessLogic
{
    public interface IDungeonRunner
    {
        Task<DungeonResultViewModel> RunDungeonAsync(RunDungeonAction runDungeonAction);
    }
}
