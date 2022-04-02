using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public class ActionCombatTimePercentageCombatEffectProcessor : IIterationsUntilNextActionCombatEffectProcessor
    {
        public CombatEffectType CombatEffectType => CombatEffectType.ActionCombatTimePercentage;

        public int ModifyIterationsUntilNextAction(int iterationsUntilNextAction, CombatEffect combatEffect, CharacterBase character)
        {
            if (combatEffect is not PermanentCombatEffect)
            {
                character.CombatEffects.Remove(combatEffect);
            }

            if (iterationsUntilNextAction == 1)
            {
                return iterationsUntilNextAction;
            }

            return (int)(iterationsUntilNextAction * (combatEffect.Value / 100.0));
        }
    }
}
