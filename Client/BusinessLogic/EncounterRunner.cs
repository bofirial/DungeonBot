using System.Threading.Tasks;
using DungeonBot.Client.BusinessLogic.EnemyActionModules;
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
            var enemy = CreateEnemy(encounter);

            while (!EncounterHasCompleted(dungeonBot, enemy))
            {
                await _encounterRoundRunner.RunEncounterRoundAsync(dungeonBot, enemy);
            }

            return new EncounterResult(dungeonBot.CurrentHealth > 0);
        }

        private static Enemy CreateEnemy(Encounter encounter)
        {
            switch (encounter.Name)
            {
                case "Big Rat":
                    return new Enemy(encounter.Name, 80, new AttackOnlyActionModule());
                case "Hungry Dragon Whelp":
                    return new Enemy(encounter.Name, 100, new AttackOnlyActionModule());
                case "Wolf King":
                    return new Enemy(encounter.Name, 80, new AttackOnlyActionModule());
                default:
                    throw new System.Exception($"Unknown Enemy: {encounter.Name}");
            }
        }
    }
}
