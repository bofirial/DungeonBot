using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public class StunnedCombatEffectProcessor : IBeforeActionCombatEffectProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;

        public StunnedCombatEffectProcessor(ICombatLogEntryBuilder combatLogEntryBuilder)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
        }

        public CombatEffectType CombatEffectType => CombatEffectType.Stunned;

        public BeforeActionCombatEffectProcessorResult ProcessBeforeActionCombatEffect(CombatEffect combatEffect, CharacterBase character, IAction action, CombatContext combatContext)
        {
            combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry($"{character.Name} is stunned.", character, combatContext));
            combatContext.NewCombatEvents.Add(new CombatEvent(combatContext.CombatTimer + combatEffect.Value, character, CombatEventType.CharacterAction));

            if (combatEffect is not PermanentCombatEffect)
            {
                character.CombatEffects.Remove(combatEffect);
            }

            return new BeforeActionCombatEffectProcessorResult(PreventAction: true);
        }
    }
}
