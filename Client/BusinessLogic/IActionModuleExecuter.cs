using System.Threading.Tasks;
using DungeonBot.Models.Combat;

namespace DungeonBot.Client.BusinessLogic
{
    public interface IActionModuleExecuter
    {
        Task<IAction> ExecuteActionModule(Player dungeonBot, ActionComponent actionComponent, SensorComponent sensorComponent);
    }
}
