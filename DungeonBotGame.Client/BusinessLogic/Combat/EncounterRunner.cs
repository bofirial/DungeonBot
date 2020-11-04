using System.Collections.Generic;
using System.Threading.Tasks;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface IEncounterRunner
    {
        Task<EncounterResultViewModel> RunAdventureEncounterAsync(DungeonBot dungeonBot, EncounterViewModel encounter);

        bool EncounterHasCompleted(DungeonBot dungeonBot, Enemy enemy, int roundCounter);
    }

    public class EncounterRunner : IEncounterRunner
    {
        private const int MAX_ROUNDS = 100;
        private readonly IEncounterRoundRunner _encounterRoundRunner;
        private readonly IEnemyFactory _enemyFactory;

        public EncounterRunner(IEncounterRoundRunner encounterRoundRunner, IEnemyFactory enemyFactory)
        {
            _encounterRoundRunner = encounterRoundRunner;
            _enemyFactory = enemyFactory;
        }

        public bool EncounterHasCompleted(DungeonBot dungeonBot, Enemy enemy, int roundCounter) => dungeonBot.CurrentHealth <= 0 || enemy.CurrentHealth <= 0 || roundCounter >= MAX_ROUNDS;

        public async Task<EncounterResultViewModel> RunAdventureEncounterAsync(DungeonBot dungeonBot, EncounterViewModel encounter)
        {
            var enemy = _enemyFactory.CreateEnemy(encounter);
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

            var resultDisplayText = string.Empty;
            var success = false;

            if (dungeonBot.CurrentHealth <= 0)
            {
                resultDisplayText = $"{enemy.Name} defeated {dungeonBot.Name}.";
                success = false;
            }
            else if (enemy.CurrentHealth <= 0)
            {
                resultDisplayText = $"{dungeonBot.Name} defeated {enemy.Name}.";
                success = true;
            }
            else if (roundCounter >= MAX_ROUNDS)
            {
                resultDisplayText = $"{dungeonBot.Name} failed to defeat {enemy.Name} in time.";
                success = false;
            }

            var characters = new List<CharacterBase>() { dungeonBot, enemy };

            return new EncounterResultViewModel(encounter.Name, encounter.Order, success: success, encounterRoundResults, resultDisplayText, characters);
        }
    }
}
