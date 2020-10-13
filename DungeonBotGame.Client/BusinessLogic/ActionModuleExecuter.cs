
using System.Threading.Tasks;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic
{
    public class ActionModuleExecuter : IActionModuleExecuter
    {
        public Task<IAction> ExecuteActionModule(DungeonBot dungeonBot, ActionComponent actionComponent, SensorComponent sensorComponent)
        {
            var parameters = new object?[] { actionComponent, sensorComponent };

            //TODO: Cancellation Token if method takes too long

            var result = (IAction)dungeonBot.ActionModuleContext.ActionModuleEntryPointMethodInfo.Invoke(dungeonBot.ActionModuleContext.ActionModuleObject, parameters);

            return Task.FromResult(result);
        }

        public Task<IAction> ExecuteEnemyActionModule(Enemy enemy, ActionComponent actionComponent, SensorComponent sensorComponent)
        {
            var result = enemy.EnemyActionModule.Action(actionComponent, sensorComponent);

            return Task.FromResult(result);
        }
    }
}
