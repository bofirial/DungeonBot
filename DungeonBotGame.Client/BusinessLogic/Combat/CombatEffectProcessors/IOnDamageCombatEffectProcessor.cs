using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public interface IOnDamageCombatEffectProcessor : ICombatEffectProcessor
    {
        public void ProcessCombatEffect(CombatEffect combatEffect, CharacterBase character, CharacterBase target, int combatDamage, CombatContext combatContext);
    }
}
