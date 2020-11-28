using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors;
using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public class CharacterActionCombatEventProcessor : ICombatEventProcessor
    {
        private readonly IActionModuleExecuter _actionModuleExecuter;
        private readonly ICombatActionProcessor _combatActionProcessor;
        private readonly ICombatValueCalculator _combatValueCalculator;
        private readonly ICombatEffectDirector _combatEffectDirector;
        private readonly IDictionary<CombatEffectType, IBeforeActionCombatEffectProcessor> _beforeActionCombatEffectProcessors;

        public CharacterActionCombatEventProcessor(IActionModuleExecuter actionModuleExecuter, ICombatActionProcessor combatActionProcessor, ICombatValueCalculator combatValueCalculator, ICombatEffectDirector combatEffectDirector, IEnumerable<IBeforeActionCombatEffectProcessor> beforeActionCombatEffectProcessors)
        {
            _actionModuleExecuter = actionModuleExecuter;
            _combatActionProcessor = combatActionProcessor;
            _combatValueCalculator = combatValueCalculator;
            _combatEffectDirector = combatEffectDirector;

            _beforeActionCombatEffectProcessors = beforeActionCombatEffectProcessors.ToDictionary(b => b.CombatEffectType, b => b);
        }

        public CombatEventType CombatEventType => CombatEventType.CharacterAction;

        public async Task ProcessCombatEvent(CombatEvent combatEvent, CombatContext combatContext)
        {
            if (combatEvent.Character.CurrentHealth <= 0)
            {
                return;
            }

            _combatEffectDirector.ProcessCombatEffects(combatEvent.Character, _beforeActionCombatEffectProcessors,
                (processor, combatEffect) => processor.ProcessCombatEffect(combatEffect, combatEvent.Character, combatContext));

            var actionComponent = new ActionComponent(combatEvent.Character);

            var sensorComponent = new SensorComponent(
                combatContext.DungeonBots.Where(d => d.CurrentHealth > 0).Cast<IDungeonBot>().ToImmutableList(),
                combatContext.Enemies.Where(e => e.CurrentHealth > 0).Cast<IEnemy>().ToImmutableList(),
                combatContext.CombatTimer,
                combatContext.CombatLog.Cast<ICombatLogEntry>().ToImmutableList());

            var action = combatEvent.Character switch
            {
                DungeonBot dungeonBot => await _actionModuleExecuter.ExecuteActionModule(dungeonBot, actionComponent, sensorComponent),
                Enemy enemy => await _actionModuleExecuter.ExecuteEnemyActionModule(enemy, actionComponent, sensorComponent),
                _ => throw new UnknownCharacterTypeException($"Unknown Character Type: {combatEvent.Character.GetType()}"),
            };

            _combatActionProcessor.ProcessAction(action, combatEvent.Character, combatContext);

            combatContext.NewCombatEvents.Add(combatEvent with { CombatTime = combatContext.CombatTimer + _combatValueCalculator.GetIterationsUntilNextAction(combatEvent.Character) });
        }
    }
}
