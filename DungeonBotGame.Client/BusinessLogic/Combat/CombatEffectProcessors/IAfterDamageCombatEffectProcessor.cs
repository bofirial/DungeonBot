using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public interface IAfterDamageCombatEffectProcessor : ICombatEffectProcessor
    {
        public void ProcessAfterDamageCombatEffect(CombatEffect combatEffect, CharacterBase character, CharacterBase target, int combatDamage, CombatContext combatContext);
    }
}
