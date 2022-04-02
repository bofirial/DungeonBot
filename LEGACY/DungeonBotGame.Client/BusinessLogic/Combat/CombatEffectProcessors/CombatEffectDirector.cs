using System;
using System.Collections.Generic;
using System.Linq;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors
{
    public interface ICombatEffectDirector
    {
        void ProcessCombatEffects<TProcessor>(CharacterBase character, IDictionary<CombatEffectType, TProcessor> combatEffectProcessors, Action<TProcessor, CombatEffect> processorAction);
    }

    public class CombatEffectDirector : ICombatEffectDirector
    {
        public void ProcessCombatEffects<TProcessor>(CharacterBase character, IDictionary<CombatEffectType, TProcessor> combatEffectProcessors, Action<TProcessor, CombatEffect> processorAction)
        {
            foreach (var combatEffect in character.CombatEffects.ToList())
            {
                if (combatEffectProcessors.ContainsKey(combatEffect.CombatEffectType))
                {
                    processorAction(combatEffectProcessors[combatEffect.CombatEffectType], combatEffect);
                }
            }
        }
    }
}
