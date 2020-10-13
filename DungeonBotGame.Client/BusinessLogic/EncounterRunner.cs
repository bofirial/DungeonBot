using System.Collections.Generic;
using System.Threading.Tasks;
using DungeonBotGame.Client.BusinessLogic.EnemyActionModules;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.Models.Display;

namespace DungeonBotGame.Client.BusinessLogic
{
    public class EncounterRunner : IEncounterRunner
    {
        private const int MAX_ROUNDS = 100;
        private readonly IEncounterRoundRunner _encounterRoundRunner;

        public EncounterRunner(IEncounterRoundRunner encounterRoundRunner)
        {
            _encounterRoundRunner = encounterRoundRunner;
        }

        public bool EncounterHasCompleted(DungeonBot dungeonBot, Enemy enemy, int roundCounter) => dungeonBot.CurrentHealth <= 0 || enemy.CurrentHealth <= 0 || roundCounter >= MAX_ROUNDS;

        public async Task<EncounterResult> RunDungeonEncounterAsync(DungeonBot dungeonBot, Encounter encounter)
        {
            var enemy = CreateEnemy(encounter);
            var encounterRoundResults = new List<EncounterRoundResult>()
            {
                new EncounterRoundResult()
                {
                    Round = 0,
                    DungeonBotCurrentHealth = dungeonBot.CurrentHealth,
                    EnemyCurrentHealth = enemy.CurrentHealth,
                    ActionResults = new List<ActionResult>()
                    {
                        new ActionResult()
                        {
                            DisplayText = $"{dungeonBot.Name} entered combat."
                        },
                        new ActionResult()
                        {
                            DisplayText = $"{enemy.Name} entered combat."
                        },
                    }
                }
            };

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

            if (dungeonBot.CurrentHealth <= 0)
            {
                encounterResult.ResultDisplayText = $"{enemy.Name} defeated {dungeonBot.Name}.";
            }
            else if (enemy.CurrentHealth <= 0)
            {
                encounterResult.ResultDisplayText = $"{dungeonBot.Name} defeated {enemy.Name}.";
            }
            else if (roundCounter >= MAX_ROUNDS)
            {
                encounterResult.ResultDisplayText = $"{dungeonBot.Name} failed to defeat {enemy.Name} in time.";
            }

            return encounterResult;
        }

        private static Enemy CreateEnemy(Encounter encounter)
        {
            return encounter.Name switch
            {
                "Big Rat" => new Enemy(encounter.Name, 80, new AttackOnlyActionModule(), new Dictionary<AbilityType, AbilityContext>()),
                "Hungry Dragon Whelp" => new Enemy(encounter.Name, MAX_ROUNDS, new AttackOnlyActionModule(), new Dictionary<AbilityType, AbilityContext>()),
                "Wolf King" => new Enemy(encounter.Name, 80, new WolfKingActionModule(), new Dictionary<AbilityType, AbilityContext>() {
                        { AbilityType.LickWounds, new AbilityContext() { MaximumCooldownRounds = 0 } }
                    }),
                //TODO: Specific Exception Types
                _ => throw new System.Exception($"Unknown Enemy: {encounter.Name}"),
            };
        }
    }
}
