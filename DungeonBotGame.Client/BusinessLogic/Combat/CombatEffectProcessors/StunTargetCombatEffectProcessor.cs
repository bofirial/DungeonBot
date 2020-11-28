using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public class StunTargetCombatEffectProcessor : IBeforeActionCombatEffectProcessor, IAfterActionCombatEffectProcessor
    {
        public CombatEffectType CombatEffectType => CombatEffectType.StunTarget;

        public BeforeActionCombatEffectProcessorResult ProcessBeforeActionCombatEffect(CombatEffect combatEffect, CharacterBase character, IAction action, CombatContext combatContext)
        {
            if (action is ITargettedAbilityAction targettedAbilityAction && targettedAbilityAction.Target is CharacterBase target)
            {
                target.CombatEffects.Add(new CombatEffect("Stunned", "Stunned", CombatEffectType.Stunned, combatEffect.Value));

                character.CombatEffects.Remove(combatEffect);
            }

            return new BeforeActionCombatEffectProcessorResult(PreventAction: false);
        }

        public void ProcessAfterActionCombatEffect(CombatEffect combatEffect, CharacterBase character, IAction action, CombatContext combatContext) => character.CombatEffects.Remove(combatEffect);
    }
}
