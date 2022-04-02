using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.AbilityProcessors
{
    public class ProtectBabiesAbilityProcessor : IPassiveAbilityProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;

        public ProtectBabiesAbilityProcessor(ICombatLogEntryBuilder combatLogEntryBuilder)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
        }

        public AbilityType AbilityType => AbilityType.ProtectBabies;

        public void ProcessAction(CharacterBase character, CombatContext combatContext)
        {
            character.CombatEffects.Add(new PermanentCombatEffect("Protect Babies", "Protect Babies", CombatEffectType.ProtectBabies, Value: 1));

            combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry($"{character.Name} is protective of her babies.", character, combatContext));
        }
    }
}
