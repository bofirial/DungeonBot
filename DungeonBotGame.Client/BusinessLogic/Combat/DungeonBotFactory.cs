using System.Linq;
using DungeonBotGame.Client.BusinessLogic.Compilation;
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
        private readonly IActionModuleContextProvider _actionModuleContextProvider;
        private readonly IAbilityDescriptionProvider _abilityDescriptionProvider;

        public DungeonBotFactory(IActionModuleContextProvider actionModuleContextProvider, IAbilityDescriptionProvider abilityDescriptionProvider)
        {
            _actionModuleContextProvider = actionModuleContextProvider;
            _abilityDescriptionProvider = abilityDescriptionProvider;
        }

        public DungeonBot CreateCombatDungeonBot(DungeonBotViewModel dungeonBot)
        {
            return new DungeonBot(
                dungeonBot.Name,
                100,
                _actionModuleContextProvider.GetActionModuleContext(dungeonBot.ActionModuleLibrary),
                dungeonBot.Abilities.ToDictionary(abilityType => abilityType, abilityType => CreateAbilityContext(abilityType))
            );
        }

        private AbilityContext CreateAbilityContext(AbilityType abilityType) => new AbilityContext() { MaximumCooldownRounds = _abilityDescriptionProvider.GetAbilityDescription(abilityType).CooldownRounds };
    }
}
