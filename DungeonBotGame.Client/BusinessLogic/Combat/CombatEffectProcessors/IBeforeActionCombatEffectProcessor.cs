using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public interface IBeforeActionCombatEffectProcessor : ICombatEffectProcessor
    {
        public BeforeActionCombatEffectProcessorResult ProcessBeforeActionCombatEffect(CombatEffect combatEffect, CharacterBase character, IAction action, CombatContext combatContext);
    }
}
