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

        public async Task RunEncounterRoundAsync(Player dungeonBot, Enemy enemy)
        {
            var actionComponent = new ActionComponent();
            var sensorComponent = new SensorComponent(dungeonBot, enemy);

            var playerAction = await _actionModuleExecuter.ExecuteActionModule(dungeonBot, actionComponent, sensorComponent);

            ProcessAction(playerAction, dungeonBot, enemy);

            var enemyAction = await _actionModuleExecuter.ExecuteEnemyActionModule(enemy, actionComponent, sensorComponent);

            ProcessAction(enemyAction, enemy, dungeonBot);
        }

        private void ProcessAction(IAction action, CharacterBase source, CharacterBase target)
        {
            if (action.ActionType == ActionType.Attack)
            {
                target.CurrentHealth -= _combatValueCalculator.GetAttackValue(source, target);
            }
            else if (action.ActionType == ActionType.Ability && action is IAbilityAction abilityAction)
            {
                //TODO: Validate the Character has the requested ability

                switch (abilityAction.AbilityType)
                {
                    case AbilityType.HeavySwing:
                        target.CurrentHealth -= _combatValueCalculator.GetAbilityValue(source, target, abilityAction.AbilityType);
                        break;
                    case AbilityType.LickWounds:
                        source.CurrentHealth = source.MaximumHealth;
                        break;
                    default:
                        throw new System.Exception($"Unknown Ability: {abilityAction.AbilityType}");
                }
            }
        }
    }
}
