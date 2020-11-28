using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.AbilityProcessors
{
    public class AnalyzeSituationAbilityProcessor : IAbilityProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;

        public AnalyzeSituationAbilityProcessor(ICombatLogEntryBuilder combatLogEntryBuilder)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
        }

        public AbilityType AbilityType => AbilityType.AnalyzeSituation;

        public void ProcessAction(IAbilityAction abilityAction, CharacterBase character, CombatContext combatContext)
        {
            character.CombatEffects.Add(new CombatEffect("Situational Analysis - Double Attack Damage", "2x Damage", CombatEffectType.AttackPercentage, Value: 200));
            character.CombatEffects.Add(new CombatEffect("Situational Analysis - Quick Follow Up Action", "2x Speed", CombatEffectType.ActionCombatTimePercentage, Value: 50));

            combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry<IAction>($"{character.Name} performs combat analysis.", character, combatContext, abilityAction));
        }
    }
}
