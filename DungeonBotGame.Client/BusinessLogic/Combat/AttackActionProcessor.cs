using System.Linq;
using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public class AttackActionProcessor : IActionProcessor
    {
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;
        private readonly ICombatValueCalculator _combatValueCalculator;

        public AttackActionProcessor(ICombatLogEntryBuilder combatLogEntryBuilder, ICombatValueCalculator combatValueCalculator)
        {
            _combatLogEntryBuilder = combatLogEntryBuilder;
            _combatValueCalculator = combatValueCalculator;
        }

        public ActionType ActionType => ActionType.Attack;

        public void ProcessAction(IAction action, CharacterBase character, CombatContext combatContext)
        {
            if (action is AttackAction attackAction && attackAction.Target is CharacterBase target)
            {
                var attackDamage = _combatValueCalculator.GetAttackValue(character, target);

                target.CurrentHealth -= attackDamage;

                _combatValueCalculator.ClampCharacterHealth(target);

                var salvageStrikesCombatEffectTypes = new CombatEffectType[] { CombatEffectType.SalvageStrikes };
                var salvageStrikesCombatEffects = character.CombatEffects.Where(c => salvageStrikesCombatEffectTypes.Contains(c.CombatEffectType)).ToList();

                foreach (var salvageStrikesCombatEffect in salvageStrikesCombatEffects)
                {
                    switch (salvageStrikesCombatEffect.CombatEffectType)
                    {
                        case CombatEffectType.SalvageStrikes:
                            var combatEffect = new CombatEffect("Salvage Strikes", CombatEffectType.DamageOverTime, (short)(attackDamage * 0.05), CombatTime: combatContext.CombatTimer + 200, CombatTimeInterval: 100);
                            target.CombatEffects.Add(combatEffect);

                            combatContext.NewCombatEvents.Add(new CombatEvent<CombatEffect>(combatContext.CombatTimer + 100, target, CombatEventType.CombatEffect, combatEffect));

                            character.CurrentHealth += (short)(attackDamage * 0.05);

                            _combatValueCalculator.ClampCharacterHealth(character);
                            break;
                    }
                }

                combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry($"{character.Name} attacked {target.Name} for {attackDamage} damage.", character, combatContext, action));
            }
            else
            {
                throw new UnknownActionTypeException("Actions with an ActionType of Attack must be AttackActions and target a Character.");
            }
        }
    }
}
