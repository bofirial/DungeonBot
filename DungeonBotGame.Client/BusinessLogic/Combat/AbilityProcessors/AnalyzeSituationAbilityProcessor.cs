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
            character.CombatEffects.Add(new CombatEffect("Situational Analysis - Attack Damage", CombatEffectType.AttackPercentage, Value: 200, CombatTime: null, CombatTimeInterval: null));
            character.CombatEffects.Add(new CombatEffect("Situational Analysis - Action Time", CombatEffectType.ActionCombatTimePercentage, Value: 50, CombatTime: null, CombatTimeInterval: null));

            combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry<IAction>($"{character.Name} performs combat analysis.", character, combatContext, abilityAction));
        }
    }
}
