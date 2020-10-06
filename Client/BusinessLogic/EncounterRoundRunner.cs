using System.Threading.Tasks;
using DungeonBot.Models.Combat;

namespace DungeonBot.Client.BusinessLogic
{
    public class EncounterRoundRunner : IEncounterRoundRunner
    {
        private readonly IActionModuleExecuter _actionModuleExecuter;

        public EncounterRoundRunner(IActionModuleExecuter actionModuleExecuter)
        {
            _actionModuleExecuter = actionModuleExecuter;
        }

        public async Task RunEncounterRoundAsync(Player dungeonBot, Enemy enemy)
        {
            var actionComponent = new ActionComponent();
            var sensorComponent = new SensorComponent(enemy);

            var result = await _actionModuleExecuter.ExecuteActionModule(dungeonBot, actionComponent, sensorComponent);

            dungeonBot.CurrentHealth -= 10;

            if (result.ActionType == ActionType.Attack)
            {
                enemy.CurrentHealth -= 10;
            }
        }
    }
}
