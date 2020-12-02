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
        private readonly ICombatValueCalculator _combatValueCalculator;
        private readonly ICombatEffectDirector _combatEffectDirector;
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;
        private readonly IDictionary<ActionType, IActionProcessor> _actionProcessors;
        private readonly IDictionary<CombatEffectType, IBeforeActionCombatEffectProcessor> _beforeActionCombatEffectProcessors;
        private readonly IDictionary<CombatEffectType, IAfterActionCombatEffectProcessor> _afterActionCombatEffectProcessors;
        private readonly IDictionary<CombatEffectType, IAfterCharacterFallsCombatEffectProcessor> _afterCharacterFallsCombatEffectProcessors;

        public CharacterActionCombatEventProcessor(IActionModuleExecuter actionModuleExecuter,
            ICombatValueCalculator combatValueCalculator,
            ICombatEffectDirector combatEffectDirector,
            ICombatLogEntryBuilder combatLogEntryBuilder,
            IEnumerable<IActionProcessor> actionProcessors,
            IEnumerable<IBeforeActionCombatEffectProcessor> beforeActionCombatEffectProcessors,
            IEnumerable<IAfterActionCombatEffectProcessor> afterActionCombatEffectProcessors,
            IEnumerable<IAfterCharacterFallsCombatEffectProcessor> afterCharacterFallsCombatEffectProcessors)
        {
            _actionModuleExecuter = actionModuleExecuter;
            _combatValueCalculator = combatValueCalculator;
            _combatEffectDirector = combatEffectDirector;
            _combatLogEntryBuilder = combatLogEntryBuilder;
            _actionProcessors = actionProcessors.ToDictionary(p => p.ActionType, p => p);
            _beforeActionCombatEffectProcessors = beforeActionCombatEffectProcessors.ToDictionary(p => p.CombatEffectType, p => p);
            _afterActionCombatEffectProcessors = afterActionCombatEffectProcessors.ToDictionary(p => p.CombatEffectType, p => p);
            _afterCharacterFallsCombatEffectProcessors = afterCharacterFallsCombatEffectProcessors.ToDictionary(p => p.CombatEffectType, p => p);
        }

        public CombatEventType CombatEventType => CombatEventType.CharacterAction;

        public async Task ProcessCombatEvent(CombatEvent combatEvent, CombatContext combatContext)
        {
            if (combatEvent.Character.CurrentHealth <= 0)
            {
                return;
            }

            var action = await ExecuteActionModuleForEventCharacter(combatEvent, combatContext);

            var preventAction = false;

            void ProcessBeforeActionCombatEffect(IBeforeActionCombatEffectProcessor beforeActionCombatEffectProcessor, CombatEffect combatEffect)
            {
                var result = beforeActionCombatEffectProcessor.ProcessBeforeActionCombatEffect(combatEffect, combatEvent.Character, action, combatContext);

                preventAction &= result.PreventAction;
            }

            _combatEffectDirector.ProcessCombatEffects(combatEvent.Character, _beforeActionCombatEffectProcessors, ProcessBeforeActionCombatEffect);

            if (preventAction)
            {
                return;
            }

            var fallenCharactersBefore = combatContext.Characters.Where(c => c.CurrentHealth <= 0).ToList();

            if (_actionProcessors.ContainsKey(action.ActionType))
            {
                _actionProcessors[action.ActionType].ProcessAction(action, combatEvent.Character, combatContext);
            }
            else
            {
                throw new UnknownActionTypeException(action.ActionType);
            }

            var fallenCharactersAfter = combatContext.Characters.Where(c => c.CurrentHealth <= 0);
            var newlyFallenCharacters = fallenCharactersAfter.Where(c => !fallenCharactersBefore.Contains(c));

            combatContext.CombatLog.AddRange(newlyFallenCharacters.Select(c => _combatLogEntryBuilder.CreateCombatLogEntry($"{c.Name} has fallen.", c, combatContext)));

            foreach (var fallenCharacter in newlyFallenCharacters)
            {
                foreach (var character in combatContext.Characters)
                {
                    _combatEffectDirector.ProcessCombatEffects(character, _afterCharacterFallsCombatEffectProcessors,
                        (processor, combatEffect) => processor.ProcessAfterCharacterFallsCombatEffect(combatEffect, character, fallenCharacter, combatContext));
                }
            }

            _combatEffectDirector.ProcessCombatEffects(combatEvent.Character, _afterActionCombatEffectProcessors,
                (processor, combatEffect) => processor.ProcessAfterActionCombatEffect(combatEffect, combatEvent.Character, action, combatContext));

            combatContext.NewCombatEvents.Add(combatEvent with
            {
                CombatTime = combatContext.CombatTimer + _combatValueCalculator.GetIterationsUntilNextAction(combatEvent.Character)
            });
        }

        private async Task<IAction> ExecuteActionModuleForEventCharacter(CombatEvent combatEvent, CombatContext combatContext)
        {
            var actionComponent = new ActionComponent(combatEvent.Character);

            var sensorComponent = new SensorComponent(
                combatContext.DungeonBots.Where(d => d.CurrentHealth > 0).Cast<IDungeonBot>().ToImmutableList(),
                combatContext.Enemies.Where(e => e.CurrentHealth > 0).Cast<IEnemy>().ToImmutableList(),
                combatContext.CombatTimer,
                combatContext.CombatLog.Cast<ICombatLogEntry>().ToImmutableList());

            return combatEvent.Character switch
            {
                DungeonBot dungeonBot => await _actionModuleExecuter.ExecuteActionModule(dungeonBot, actionComponent, sensorComponent),
                Enemy enemy => await _actionModuleExecuter.ExecuteEnemyActionModule(enemy, actionComponent, sensorComponent),
                _ => throw new UnknownCharacterTypeException($"Unknown Character Type: {combatEvent.Character.GetType()}"),
            };
        }
    }
}
