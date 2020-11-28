using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public interface IAttackValueCombatEffectProcessor : ICombatEffectProcessor
    {
        public int ModifyAttackValue(int attackValue, CombatEffect combatEffect, CharacterBase character);
    }
}
