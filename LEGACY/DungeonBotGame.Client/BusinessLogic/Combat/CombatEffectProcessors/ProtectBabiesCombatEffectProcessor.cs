using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public class ProtectBabiesCombatEffectProcessor : IAfterCharacterFallsCombatEffectProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;

        public ProtectBabiesCombatEffectProcessor(ICombatLogEntryBuilder combatLogEntryBuilder)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
        }

        public CombatEffectType CombatEffectType => CombatEffectType.ProtectBabies;

        public void ProcessAfterCharacterFallsCombatEffect(CombatEffect combatEffect, CharacterBase character, CharacterBase fallenCharacter, CombatContext combatContext)
        {
            if (fallenCharacter is DungeonBot == character is DungeonBot)
            {
                character.CombatEffects.Add(new PermanentCombatEffect("Mama's Rage", "10x Damage", CombatEffectType.AttackPercentage, 1000));

                _combatLogEntryBuilder.CreateCombatLogEntry($"{character.Name} goes into an intense rage after seeing {fallenCharacter.Name} fall.", character, combatContext);
            }
        }
    }
}
