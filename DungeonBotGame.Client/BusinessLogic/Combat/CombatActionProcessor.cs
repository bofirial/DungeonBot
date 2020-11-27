using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface ICombatActionProcessor
    {
        void ProcessAction(IAction action, CharacterBase source, CombatContext combatContext);
    }

    public class CombatActionProcessor : ICombatActionProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;

        private readonly IDictionary<ActionType, IActionProcessor> _actionProcessors;

        public CombatActionProcessor(IEnumerable<IActionProcessor> actionProcessors, ICombatLogEntryBuilder combatLogEntryBuilder)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;

            _actionProcessors = actionProcessors.ToDictionary(a => a.ActionType, a => a);
        }

        public void ProcessAction(IAction action, CharacterBase source, CombatContext combatContext)
        {
            var fallenCharactersBefore = combatContext.Characters.Where(c => c.CurrentHealth <= 0).ToList();

            if (_actionProcessors.ContainsKey(action.ActionType))
            {
                _actionProcessors[action.ActionType].ProcessAction(action, source, combatContext);
            }
            else
            {
                throw new UnknownActionTypeException(action.ActionType);
            }

            if (source.CurrentHealth < 0)
            {
                source.CurrentHealth = 0;
            }

            var fallenCharactersAfter = combatContext.Characters.Where(c => c.CurrentHealth <= 0);
            var newlyFallenCharacters = fallenCharactersAfter.Where(c => !fallenCharactersBefore.Contains(c));

            combatContext.CombatLog.AddRange(newlyFallenCharacters.Select(c => _combatLogEntryBuilder.CreateCombatLogEntry($"{c.Name} has fallen.", c, combatContext)));

            var removeAfterActionEffectTypes = new CombatEffectType[] { CombatEffectType.StunTarget };
            var removeAfterActionEffects = source.CombatEffects.Where(c => removeAfterActionEffectTypes.Contains(c.CombatEffectType)).ToList();

            foreach (var removeAfterActionEffect in removeAfterActionEffects)
            {
                source.CombatEffects.Remove(removeAfterActionEffect);
            }
        }
    }
}
