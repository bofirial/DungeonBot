using System.Threading.Tasks;
using DungeonBot.Client.Store.Dungeons;
using DungeonBot.Models.Display;

namespace DungeonBot.Client.BusinessLogic
{
    public interface IDungeonRunner
    {
        Task<DungeonResult> RunDungeonAsync(RunDungeonAction runDungeonAction);
    }
}
