using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.AbilityProcessors
{
    public class SurpriseAttackAbilityProcessor : IPassiveAbilityProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;

        public SurpriseAttackAbilityProcessor(ICombatLogEntryBuilder combatLogEntryBuilder)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
        }

        public AbilityType AbilityType => AbilityType.SurpriseAttack;

        public void ProcessAction(CharacterBase character, CombatContext combatContext)
        {
            character.CombatEffects.Add(new CombatEffect("Element of Surprise - Double Attack Damage", "2x Damage", CombatEffectType.AttackPercentage, Value: 200));
            character.CombatEffects.Add(new CombatEffect("Element of Surprise - Immediate Action", "Instant Action", CombatEffectType.ImmediateAction, Value: 1));
            character.CombatEffects.Add(new CombatEffect("Element of Surprise - Stun Target", "Stun Target", CombatEffectType.StunTarget, Value: 200));

            combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry($"{character.Name} has the element of surprise.", character, combatContext));
        }
    }
}
