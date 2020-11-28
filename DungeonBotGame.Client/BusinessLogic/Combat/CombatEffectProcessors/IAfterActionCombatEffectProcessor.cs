using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public interface IAfterActionCombatEffectProcessor : ICombatEffectProcessor
    {
        public void ProcessAfterActionCombatEffect(CombatEffect combatEffect, CharacterBase character, IAction action, CombatContext combatContext);
    }
}
