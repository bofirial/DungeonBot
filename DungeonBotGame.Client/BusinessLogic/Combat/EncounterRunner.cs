﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DungeonBotGame.Client.BusinessLogic.EnemyActionModules;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic.Combat
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

        public async Task<EncounterResultViewModel> RunAdventureEncounterAsync(DungeonBot dungeonBot, EncounterViewModel encounter)
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

            var resultDisplayText = string.Empty;

            if (dungeonBot.CurrentHealth <= 0)
            {
                resultDisplayText = $"{enemy.Name} defeated {dungeonBot.Name}.";
            }
            else if (enemy.CurrentHealth <= 0)
            {
                resultDisplayText = $"{dungeonBot.Name} defeated {enemy.Name}.";
            }
            else if (roundCounter >= MAX_ROUNDS)
            {
                resultDisplayText = $"{dungeonBot.Name} failed to defeat {enemy.Name} in time.";
            }

            return new EncounterResultViewModel(success: dungeonBot.CurrentHealth > 0 && roundCounter < MAX_ROUNDS, encounterRoundResults, resultDisplayText);
        }

        private static Enemy CreateEnemy(EncounterViewModel encounter)
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
