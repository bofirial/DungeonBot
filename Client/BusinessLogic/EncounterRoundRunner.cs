using System.Collections.Generic;
using System.Threading.Tasks;
using DungeonBot.Models.Combat;

namespace DungeonBot.Client.BusinessLogic
{
    public class EncounterRoundRunner : IEncounterRoundRunner
    {
        private readonly IActionModuleExecuter _actionModuleExecuter;
        private readonly ICombatValueCalculator _combatValueCalculator;

        public EncounterRoundRunner(IActionModuleExecuter actionModuleExecuter, ICombatValueCalculator combatValueCalculator)
        {
            _actionModuleExecuter = actionModuleExecuter;
            _combatValueCalculator = combatValueCalculator;
        }

        public async Task<EncounterRoundResult> RunEncounterRoundAsync(Player dungeonBot, Enemy enemy, int roundCounter, IEnumerable<EncounterRoundResult> encounterRoundResults)
        {
            var actionComponent = new ActionComponent();
            var sensorComponent = new SensorComponent(dungeonBot, enemy, roundCounter, encounterRoundResults);

            var playerAction = await _actionModuleExecuter.ExecuteActionModule(dungeonBot, actionComponent, sensorComponent);

            var playerActionResult = ProcessAction(playerAction, dungeonBot, enemy);

            var enemyAction = await _actionModuleExecuter.ExecuteEnemyActionModule(enemy, actionComponent, sensorComponent);

            var enemyActionResult = ProcessAction(enemyAction, enemy, dungeonBot);

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

        private ActionResult ProcessAction(IAction action, CharacterBase source, CharacterBase target)
        {
            var actionResult = new ActionResult() { Action = action };

            if (action.ActionType == ActionType.Attack)
            {
                var attackDamage = _combatValueCalculator.GetAttackValue(source, target);

                target.CurrentHealth -= attackDamage;

                actionResult.DisplayText = $"{source.Name} attacked {target.Name} for {attackDamage} damage.";
            }
            else if (action.ActionType == ActionType.Ability && action is IAbilityAction abilityAction)
            {
                //TODO: Validate the Character has the requested ability
                //TODO: Validate the Ability's Cooldown

                switch (abilityAction.AbilityType)
                {
                    case AbilityType.HeavySwing:

                        var abilityDamage = _combatValueCalculator.GetAbilityValue(source, target, abilityAction.AbilityType); ;

                        target.CurrentHealth -= abilityDamage;

                        actionResult.DisplayText = $"{source.Name} took a heavy swing at {target.Name} for {abilityDamage} damage.";
                        break;

                    case AbilityType.LickWounds:

                        source.CurrentHealth = source.MaximumHealth;

                        actionResult.DisplayText = $"{source.Name} licked it's wounds because {source.Name} used an ability last turn.";
                        break;

                    default:
                        throw new System.Exception($"Unknown Ability: {abilityAction.AbilityType}");
                }
            }

            return actionResult;
        }
    }
}
