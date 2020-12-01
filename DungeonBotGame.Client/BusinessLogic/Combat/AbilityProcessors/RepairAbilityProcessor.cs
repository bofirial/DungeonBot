using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.AbilityProcessors
{
    public class RepairAbilityProcessor : IAbilityProcessor, IPassiveAbilityProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;
        private readonly ICombatValueCalculator _combatValueCalculator;
        private readonly ICombatDamageApplier _combatDamageApplier;

        public RepairAbilityProcessor(ICombatLogEntryBuilder combatLogEntryBuilder, ICombatValueCalculator combatValueCalculator, ICombatDamageApplier combatDamageApplier)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
            _combatValueCalculator = combatValueCalculator;
            _combatDamageApplier = combatDamageApplier;
        }

        public AbilityType AbilityType => AbilityType.Repair;

        public void ProcessAction(IAbilityAction abilityAction, CharacterBase character, CombatContext combatContext)
        {
            if (abilityAction is ITargettedAbilityAction targettedAbilityAction)
            {
                if (targettedAbilityAction.Target is CharacterBase target)
                {
                    var health = _combatValueCalculator.GetAttackValue(character, target) * 2;

                    _combatDamageApplier.ApplyHealing(character, target, health, combatContext);

                    var displayText = $"{character.Name} repaired {(target.Name == character.Name ? "itself" : target.Name)} {(target.CurrentHealth == target.MaximumHealth ? "completely" : $"for {health} hp")}.";
                    combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry<IAction>(displayText, character, combatContext, abilityAction));
                }
                else
                {
                    throw new InvalidTargetException(targettedAbilityAction.Target);
                }
            }
            else
            {
                throw new InvalidTargetException((ITarget?)null);
            }
        }

        public void ProcessAction(CharacterBase character, CombatContext combatContext)
        {
            foreach (var dungeonBot in combatContext.DungeonBots)
            {
                if (dungeonBot.CurrentHealth < dungeonBot.MaximumHealth)
                {
                    dungeonBot.CurrentHealth = dungeonBot.MaximumHealth;

                    combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry($"{character.Name} repaired {dungeonBot.Name} between encounters.", character, combatContext));
                }
            }
        }
    }
}
