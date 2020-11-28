﻿using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public class StunnedCombatEffectProcessor : IBeforeActionCombatEffectProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;

        public StunnedCombatEffectProcessor(ICombatLogEntryBuilder combatLogEntryBuilder)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
        }

        public CombatEffectType CombatEffectType => CombatEffectType.Stunned;

        public void ProcessCombatEffect(CombatEffect combatEffect, CharacterBase character, CombatContext combatContext)
        {
            combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry($"{character.Name} is stunned.", character, combatContext));
            combatContext.NewCombatEvents.Add(new CombatEvent(combatContext.CombatTimer + combatEffect.Value, character, CombatEventType.CharacterAction));

            character.CombatEffects.Remove(combatEffect);
        }
    }
}
