using System.Threading.Tasks;
using DungeonBot.Models.Combat;
using DungeonBot.Models.Display;

namespace DungeonBot.Client.BusinessLogic
{
    public class EncounterRunner : IEncounterRunner
    {
        private readonly IEncounterRoundRunner _encounterRoundRunner;

        public EncounterRunner(IEncounterRoundRunner encounterRoundRunner)
        {
            _encounterRoundRunner = encounterRoundRunner;
        }

        public bool EncounterHasCompleted(Player dungeonBot, Enemy enemy) => dungeonBot.CurrentHealth <= 0 || enemy.CurrentHealth <= 0;

        public async Task<EncounterResult> RunDungeonEncounterAsync(Player dungeonBot, Encounter encounter)
        {
            var enemy = new Enemy(encounter.Name, 80);

            while (!EncounterHasCompleted(dungeonBot, enemy))
            {
                await _encounterRoundRunner.RunEncounterRoundAsync(dungeonBot, enemy);
            }

            return new EncounterResult(dungeonBot.CurrentHealth > 0);
        }

    }
}
