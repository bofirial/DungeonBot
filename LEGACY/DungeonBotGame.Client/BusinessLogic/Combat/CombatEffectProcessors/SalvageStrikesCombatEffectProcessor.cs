using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public class SalvageStrikesCombatEffectProcessor : IAfterDamageCombatEffectProcessor
    {
        private readonly ICombatValueCalculator _combatValueCalculator;

        public SalvageStrikesCombatEffectProcessor(ICombatValueCalculator combatValueCalculator)
        {
            _combatValueCalculator = combatValueCalculator;
        }

        public CombatEffectType CombatEffectType => CombatEffectType.SalvageStrikes;

        public void ProcessAfterDamageCombatEffect(CombatEffect combatEffect, CharacterBase character, CharacterBase target, int combatDamage, CombatContext combatContext)
        {
            if (combatEffect is not PermanentCombatEffect)
            {
                character.CombatEffects.Remove(combatEffect);
            }

            var newCombatEffect = new TimedIntervalCombatEffect("Salvage Strikes Bleed", "Bleed", CombatEffectType.DamageOverTime, (short)(combatDamage * 0.05), CombatTime: combatContext.CombatTimer + 200, CombatTimeInterval: 100);

            target.CombatEffects.Add(newCombatEffect);

            combatContext.NewCombatEvents.Add(new CombatEffectCombatEvent(combatContext.CombatTimer + 100, target, CombatEventType.CombatEffect, newCombatEffect));

            character.CurrentHealth += (short)(combatDamage * 0.05);

            _combatValueCalculator.ClampCharacterHealth(character);
        }
    }
}
