
using System.Threading.Tasks;
using DungeonBot.Models.Combat;

namespace DungeonBot.Client.BusinessLogic
{
    public class ActionModuleExecuter : IActionModuleExecuter
    {
        public Task<IAction> ExecuteActionModule(Player dungeonBot, ActionComponent actionComponent, SensorComponent sensorComponent)
        {
            var parameters = new object?[] { actionComponent, sensorComponent };

            var result = (IAction)dungeonBot.ActionModuleContext.ActionModuleEntryPointMethodInfo.Invoke(dungeonBot.ActionModuleContext.ActionModuleObject, parameters);

            return Task.FromResult(result);
        }
    }
}
