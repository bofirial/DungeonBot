
using System.Threading.Tasks;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic
{
    public interface IActionModuleExecuter
    {
        Task<IAction> ExecuteActionModule(DungeonBot dungeonBot, ActionComponent actionComponent, SensorComponent sensorComponent);
        Task<IAction> ExecuteEnemyActionModule(Enemy enemy, ActionComponent actionComponent, SensorComponent sensorComponent);
    }

    public class ActionModuleExecuter : IActionModuleExecuter
    {
        public Task<IAction> ExecuteActionModule(DungeonBot dungeonBot, ActionComponent actionComponent, SensorComponent sensorComponent)
        {
            var parameters = new object?[] { actionComponent, sensorComponent };

            //TODO: Cancellation Token if method takes too long

            var result = dungeonBot.ActionModuleContext.ActionModuleEntryPointMethodInfo.Invoke(dungeonBot.ActionModuleContext.ActionModuleObject, parameters);

            if (result != null && result is IAction action)
            {
                return Task.FromResult(action);
            }

            throw new System.Exception("Incorrect result returned from DungeonBot Script.");
        }

        public Task<IAction> ExecuteEnemyActionModule(Enemy enemy, ActionComponent actionComponent, SensorComponent sensorComponent)
        {
            var result = enemy.EnemyActionModule.Action(actionComponent, sensorComponent);

            return Task.FromResult(result);
        }
    }
}
