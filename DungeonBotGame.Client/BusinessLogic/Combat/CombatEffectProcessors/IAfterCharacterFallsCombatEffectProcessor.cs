using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public interface IAfterCharacterFallsCombatEffectProcessor : ICombatEffectProcessor
    {
        public void ProcessAfterCharacterFallsCombatEffect(CombatEffect combatEffect, CharacterBase character, CharacterBase fallenCharacter, CombatContext combatContext);
    }
}
