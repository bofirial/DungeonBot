﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DungeonBot.Models.Combat;

namespace DungeonBot.Client.BusinessLogic
{
    public class EncounterRoundRunner : IEncounterRoundRunner
    {
        private readonly IActionModuleExecuter _actionModuleExecuter;
        private readonly ICombatActionProcessor _combatActionProcessor;

        public EncounterRoundRunner(IActionModuleExecuter actionModuleExecuter, ICombatActionProcessor combatActionProcessor)
        {
            _actionModuleExecuter = actionModuleExecuter;
            _combatActionProcessor = combatActionProcessor;
        }

        public async Task<EncounterRoundResult> RunEncounterRoundAsync(Player dungeonBot, Enemy enemy, int roundCounter, IEnumerable<EncounterRoundResult> encounterRoundResults)
        {
            var dungeonBotActionComponent = new ActionComponent(dungeonBot);
            var enemyActionComponent = new ActionComponent(enemy);
            var sensorComponent = new SensorComponent(dungeonBot, enemy, roundCounter, encounterRoundResults);

            var playerAction = await _actionModuleExecuter.ExecuteActionModule(dungeonBot, dungeonBotActionComponent, sensorComponent);

            var playerActionResult = _combatActionProcessor.ProcessAction(playerAction, dungeonBot, enemy);

            var enemyAction = await _actionModuleExecuter.ExecuteEnemyActionModule(enemy, enemyActionComponent, sensorComponent);

            var enemyActionResult = _combatActionProcessor.ProcessAction(enemyAction, enemy, dungeonBot);

            return new EncounterRoundResult()
            {
                ActionResults = new List<ActionResult>()
                {
                    playerActionResult,
                    enemyActionResult
                },
                DungeonBotCurrentHealth = dungeonBot.CurrentHealth,
                EnemyCurrentHealth = enemy.CurrentHealth
            };
        }
    }
}