using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public class ImmediateActionCombatEffectProcessor : IIterationsUntilNextActionCombatEffectProcessor
    {
        public CombatEffectType CombatEffectType => CombatEffectType.ImmediateAction;

        public int ModifyIterationsUntilNextAction(int iterationsUntilNextAction, CombatEffect combatEffect, CharacterBase character)
        {
            character.CombatEffects.Remove(combatEffect);

            return 1;
        }
    }
}
