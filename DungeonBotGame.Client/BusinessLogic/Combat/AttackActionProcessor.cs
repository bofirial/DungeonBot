using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public class AttackActionProcessor : IActionProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;
        private readonly ICombatValueCalculator _combatValueCalculator;
        private readonly ICombatDamageApplier _combatDamageApplier;

        public AttackActionProcessor(ICombatLogEntryBuilder combatLogEntryBuilder, ICombatValueCalculator combatValueCalculator, ICombatDamageApplier combatDamageApplier)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
            _combatValueCalculator = combatValueCalculator;
            _combatDamageApplier = combatDamageApplier;
        }

        public ActionType ActionType => ActionType.Attack;

        public void ProcessAction(IAction action, CharacterBase character, CombatContext combatContext)
        {
            if (action is AttackAction attackAction && attackAction.Target is CharacterBase target)
            {
                var attackDamage = _combatValueCalculator.GetAttackValue(character, target);

                _combatDamageApplier.ApplyDamage(character, target, attackDamage, combatContext);

                combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry($"{character.Name} attacked {target.Name} for {attackDamage} damage.", character, combatContext, action));
            }
            else
            {
                throw new UnknownActionTypeException("Actions with an ActionType of Attack must be AttackActions and target a Character.");
            }
        }
    }
}
