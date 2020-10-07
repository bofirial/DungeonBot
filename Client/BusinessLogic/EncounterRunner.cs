using System.Collections.Generic;
using System.Threading.Tasks;
using DungeonBot.Client.BusinessLogic.EnemyActionModules;
using DungeonBot.Models.Combat;
using DungeonBot.Models.Display;

namespace DungeonBot.Client.BusinessLogic
{
    public class EncounterRunner : IEncounterRunner
    {
        private const int MAX_ROUNDS = 100;
        private readonly IEncounterRoundRunner _encounterRoundRunner;

        public EncounterRunner(IEncounterRoundRunner encounterRoundRunner)
        {
            _encounterRoundRunner = encounterRoundRunner;
        }

        public bool EncounterHasCompleted(Player dungeonBot, Enemy enemy, int roundCounter) => dungeonBot.CurrentHealth <= 0 || enemy.CurrentHealth <= 0 || roundCounter >= MAX_ROUNDS;

        public async Task<EncounterResult> RunDungeonEncounterAsync(Player dungeonBot, Encounter encounter)
        {
            var enemy = CreateEnemy(encounter);
            var encounterRoundResults = new List<EncounterRoundResult>();

            var roundCounter = 0;

            while (!EncounterHasCompleted(dungeonBot, enemy, roundCounter))
            {
                roundCounter++;

                var encounterRoundResult = await _encounterRoundRunner.RunEncounterRoundAsync(dungeonBot, enemy, roundCounter, encounterRoundResults);

                encounterRoundResults.Add(encounterRoundResult);
            }

            var encounterResult = new EncounterResult()
            {
                Success = dungeonBot.CurrentHealth > 0 && roundCounter < MAX_ROUNDS,
                EncounterRoundResults = encounterRoundResults,
            };

            if (dungeonBot.CurrentHealth <= 0 || roundCounter >= MAX_ROUNDS)
            {
                encounterResult.ResultDisplayText = $"{enemy.Name} defeated {dungeonBot.Name}.";
            }
            else if (enemy.CurrentHealth <= 0)
            {
                encounterResult.ResultDisplayText = $"{dungeonBot.Name} defeated {enemy.Name}.";
            }

            return encounterResult;
        }

        private static Enemy CreateEnemy(Encounter encounter)
        {
            switch (encounter.Name)
            {
                case "Big Rat":
                    return new Enemy(encounter.Name, 80, new AttackOnlyActionModule());
                case "Hungry Dragon Whelp":
                    return new Enemy(encounter.Name, MAX_ROUNDS, new AttackOnlyActionModule());
                case "Wolf King":
                    return new Enemy(encounter.Name, 80, new WolfKingActionModule());
                default:
                    throw new System.Exception($"Unknown Enemy: {encounter.Name}");
            }
        }
    }
}
