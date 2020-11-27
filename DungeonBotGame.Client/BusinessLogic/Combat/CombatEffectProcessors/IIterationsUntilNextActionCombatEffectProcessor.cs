using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public interface IIterationsUntilNextActionCombatEffectProcessor : ICombatEffectProcessor
    {
        public int ModifyIterationsUntilNextAction(int iterationsUntilNextAction, CombatEffect combatEffect, CharacterBase character);
    }
}
