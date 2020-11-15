using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DungeonBotGame.Client.BusinessLogic.Compilation;
using DungeonBotGame.Client.Store.Adventures;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface IAdventureRunner
    {
        Task<AdventureResultViewModel> RunAdventureAsync(RunAdventureAction runAdventureAction);
    }

    public class AdventureRunner : IAdventureRunner
    {
        private readonly IDungeonBotFactory _dungeonBotFactory;
        private readonly IEncounterRunner _encounterRunner;
        private readonly IActionModuleContextBuilder _actionModuleContextBuilder;

        public AdventureRunner(IDungeonBotFactory dungeonBotFactory, IEncounterRunner encounterRunner, IActionModuleContextBuilder actionModuleContextBuilder)
        {
            _dungeonBotFactory = dungeonBotFactory;
            _encounterRunner = encounterRunner;
            _actionModuleContextBuilder = actionModuleContextBuilder;
        }

        public async Task<AdventureResultViewModel> RunAdventureAsync(RunAdventureAction runAdventureAction)
        {
            var dungeonBotViewModel = runAdventureAction.DungeonBot;

            if (dungeonBotViewModel.ActionModuleContext == null)
            {
                dungeonBotViewModel = await _actionModuleContextBuilder.BuildActionModuleContextAsync(dungeonBotViewModel);
            }

            var dungeonBot = _dungeonBotFactory.CreateCombatDungeonBot(dungeonBotViewModel);

            var encounterResults = new List<EncounterResultViewModel>();

            foreach (var encounter in runAdventureAction.Adventure.Encounters)
            {
                var encounterResult = await _encounterRunner.RunAdventureEncounterAsync(dungeonBot, encounter);

                encounterResults.Add(encounterResult);

                if (!encounterResult.Success)
                {
                    return new AdventureResultViewModel(runAdventureAction.RunId, Success: false, encounterResults.AsReadOnly());
                }
            }

            return new AdventureResultViewModel(runAdventureAction.RunId, Success: encounterResults.All(e => e.Success), encounterResults.AsReadOnly());
        }
    }
}
