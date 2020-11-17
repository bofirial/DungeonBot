using DungeonBotGame.Models.Combat;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface IDungeonBotFactory
    {
        DungeonBot CreateCombatDungeonBot(DungeonBotViewModel dungeonBot);
    }

    public class DungeonBotFactory : IDungeonBotFactory
    {
        private readonly IAbilityContextDictionaryBuilder _abilityContextDictionaryBuilder;
        private readonly ICombatValueCalculator _combatValueCalculator;

        public DungeonBotFactory(IAbilityContextDictionaryBuilder abilityContextDictionaryBuilder, ICombatValueCalculator combatValueCalculator)
        {
            _abilityContextDictionaryBuilder = abilityContextDictionaryBuilder;
            _combatValueCalculator = combatValueCalculator;
        }

        public DungeonBot CreateCombatDungeonBot(DungeonBotViewModel dungeonBotViewModel)
        {
            var dungeonBot = new DungeonBot(
                            dungeonBotViewModel.Name,
                            dungeonBotViewModel.Level,
                            dungeonBotViewModel.Power,
                            dungeonBotViewModel.Armor,
                            dungeonBotViewModel.Speed,
                            dungeonBotViewModel.ActionModuleFiles,
                            dungeonBotViewModel.ActionModuleContext,
                            _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(dungeonBotViewModel.Abilities)
                        );

            dungeonBot.MaximumHealth = _combatValueCalculator.GetMaximumHealth(dungeonBot);
            dungeonBot.CurrentHealth = dungeonBot.MaximumHealth;

            return dungeonBot;
        }
    }
}
