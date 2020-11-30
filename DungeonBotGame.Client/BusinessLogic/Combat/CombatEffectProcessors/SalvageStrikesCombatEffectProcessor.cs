﻿using DungeonBotGame.Models.Combat;

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
            var newCombatEffect = new CombatEffect<(int CombatTime, int CombatTimeInterval)>("Salvage Strikes Bleed", "Bleed", CombatEffectType.DamageOverTime, (short)(combatDamage * 0.05), (CombatTime: combatContext.CombatTimer + 200, CombatTimeInterval: 100));

            target.CombatEffects.Add(newCombatEffect);

            combatContext.NewCombatEvents.Add(new CombatEvent<CombatEffect>(combatContext.CombatTimer + 100, target, CombatEventType.CombatEffect, newCombatEffect));

            character.CurrentHealth += (short)(combatDamage * 0.05);

            _combatValueCalculator.ClampCharacterHealth(character);
        }
    }
}