using System.Threading.Tasks;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic
{
    public interface IActionModuleExecuter
    {
        Task<IAction> ExecuteActionModule(DungeonBot dungeonBot, ActionComponent actionComponent, SensorComponent sensorComponent);
        Task<IAction> ExecuteEnemyActionModule(Enemy enemy, ActionComponent actionComponent, SensorComponent sensorComponent);
    }
}
