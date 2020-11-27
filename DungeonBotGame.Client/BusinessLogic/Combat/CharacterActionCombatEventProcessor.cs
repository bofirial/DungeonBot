using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public class CharacterActionCombatEventProcessor : ICombatEventProcessor
    {
        private readonly IActionModuleExecuter _actionModuleExecuter;
        private readonly ICombatActionProcessor _combatActionProcessor;
        private readonly ICombatValueCalculator _combatValueCalculator;

        public CharacterActionCombatEventProcessor(IActionModuleExecuter actionModuleExecuter, ICombatActionProcessor combatActionProcessor, ICombatValueCalculator combatValueCalculator)
        {
            _actionModuleExecuter = actionModuleExecuter;
            _combatActionProcessor = combatActionProcessor;
            _combatValueCalculator = combatValueCalculator;
        }

        public CombatEventType CombatEventType => CombatEventType.CharacterAction;

        public async Task ProcessCombatEvent(CombatEvent combatEvent, CombatContext combatContext)
        {
            if (combatEvent.Character.CurrentHealth <= 0)
            {
                return;
            }

            var startOfCharacterActionCombatEffectTypes = new CombatEffectType[] { CombatEffectType.Stunned };
            var startOfCharacterActionCombatEffects = combatEvent.Character.CombatEffects.Where(c => startOfCharacterActionCombatEffectTypes.Contains(c.CombatEffectType)).ToList();

            foreach (var startOfCharacterActionCombatEffect in startOfCharacterActionCombatEffects)
            {
                switch (startOfCharacterActionCombatEffect.CombatEffectType)
                {
                    case CombatEffectType.Stunned:

                        combatContext.CombatLog.Add(new CombatLogEntry(combatContext.CombatTimer, combatEvent.Character, $"{combatEvent.Character.Name} is stunned.", null, combatContext.Characters.Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList()));
                        combatContext.NewCombatEvents.Add(combatEvent with { CombatTime = combatContext.CombatTimer + startOfCharacterActionCombatEffect.Value });

                        combatEvent.Character.CombatEffects.Remove(startOfCharacterActionCombatEffect);

                        return;
                }
            }

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
