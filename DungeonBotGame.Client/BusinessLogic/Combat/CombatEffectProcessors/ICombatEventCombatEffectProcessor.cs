using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public interface ICombatEventCombatEffectProcessor : ICombatEffectProcessor
    {
        public void ProcessCombatEvent(CombatEvent<CombatEffect> combatEffectEvent, CombatContext combatContext);
    }
}
