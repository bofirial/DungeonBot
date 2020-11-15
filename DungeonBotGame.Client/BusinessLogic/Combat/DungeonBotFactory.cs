using System.Linq;
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

        public DungeonBotFactory(IAbilityContextDictionaryBuilder abilityContextDictionaryBuilder)
        {
            _abilityContextDictionaryBuilder = abilityContextDictionaryBuilder;
        }

        public DungeonBot CreateCombatDungeonBot(DungeonBotViewModel dungeonBot)
        {
            return new DungeonBot(
                dungeonBot.Name,
                100,
                dungeonBot.ActionModuleFiles.First().Content,
                dungeonBot.ActionModuleContext,
                _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(dungeonBot.Abilities)
            );
        }
    }
}
