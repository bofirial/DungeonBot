using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public interface IBeforeActionCombatEffectProcessor : ICombatEffectProcessor
    {
        public void ProcessCombatEffect(CombatEffect combatEffect, CharacterBase character, CombatContext combatContext);
    }
}
