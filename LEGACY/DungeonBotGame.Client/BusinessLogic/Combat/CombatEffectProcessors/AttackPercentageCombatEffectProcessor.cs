using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public class AttackPercentageCombatEffectProcessor : IAttackValueCombatEffectProcessor
    {
        public CombatEffectType CombatEffectType => CombatEffectType.AttackPercentage;

        public int ModifyAttackValue(int attackValue, CombatEffect combatEffect, CharacterBase character)
        {
            if (combatEffect is not PermanentCombatEffect)
            {
                character.CombatEffects.Remove(combatEffect);
            }

            return (int)(attackValue * (combatEffect.Value / 100.0));
        }
    }
}
