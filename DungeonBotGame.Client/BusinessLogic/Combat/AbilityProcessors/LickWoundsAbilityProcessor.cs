using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.AbilityProcessors
{
    public class LickWoundsAbilityProcessor : IAbilityProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;
        private readonly ICombatValueCalculator _combatValueCalculator;

        public LickWoundsAbilityProcessor(ICombatLogEntryBuilder combatLogEntryBuilder, ICombatValueCalculator combatValueCalculator)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
            _combatValueCalculator = combatValueCalculator;
        }

        public AbilityType AbilityType => AbilityType.LickWounds;

        public void ProcessAction(IAbilityAction abilityAction, CharacterBase character, CombatContext combatContext)
        {

            character.CurrentHealth = character.MaximumHealth;

            _combatValueCalculator.ClampCharacterHealth(character);

            combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry<IAction>($"{character.Name} licked it's wounds because a DungeonBot used an ability last turn.", character, combatContext, abilityAction));
        }
    }
}
