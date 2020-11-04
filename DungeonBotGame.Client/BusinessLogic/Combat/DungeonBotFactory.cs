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
        private readonly IAbilityContextDictionaryBuilder _abilityContextDictionaryBuilder;

        public DungeonBotFactory(IActionModuleContextProvider actionModuleContextProvider, IAbilityContextDictionaryBuilder abilityContextDictionaryBuilder)
        {
            _actionModuleContextProvider = actionModuleContextProvider;
            _abilityContextDictionaryBuilder = abilityContextDictionaryBuilder;
        }

        public DungeonBot CreateCombatDungeonBot(DungeonBotViewModel dungeonBot)
        {
            return new DungeonBot(
                dungeonBot.Name,
                100,
                dungeonBot.ActionModuleLibrary.ActionModuleFiles.First().Content,
                _actionModuleContextProvider.GetActionModuleContext(dungeonBot.ActionModuleLibrary),
                _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(dungeonBot.Abilities)
            );
        }
    }
}
