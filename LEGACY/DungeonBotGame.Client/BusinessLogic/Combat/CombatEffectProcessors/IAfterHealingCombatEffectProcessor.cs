using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public interface IAfterHealingCombatEffectProcessor : ICombatEffectProcessor
    {
        public void ProcessAfterHealingCombatEffect(CombatEffect combatEffect, CharacterBase character, CharacterBase target, int combatHealing, CombatContext combatContext);
    }
}
