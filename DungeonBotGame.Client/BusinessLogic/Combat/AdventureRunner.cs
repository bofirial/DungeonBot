using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public AdventureRunner(IDungeonBotFactory dungeonBotFactory, IEncounterRunner encounterRunner)
        {
            _dungeonBotFactory = dungeonBotFactory;
            _encounterRunner = encounterRunner;
        }

        public async Task<AdventureResultViewModel> RunAdventureAsync(RunAdventureAction runAdventureAction)
        {
            var dungeonBot = _dungeonBotFactory.CreateCombatDungeonBot(runAdventureAction.DungeonBot);

            var encounterResults = new List<EncounterResultViewModel>();

            foreach (var encounter in runAdventureAction.Adventure.Encounters)
            {
                var encounterResult = await _encounterRunner.RunAdventureEncounterAsync(dungeonBot, encounter);

                encounterResults.Add(encounterResult);

                if (!encounterResult.Success)
                {
                    return new AdventureResultViewModel(runAdventureAction.RunId, success: false, encounterResults);
                }
            }

            return new AdventureResultViewModel(runAdventureAction.RunId, success: encounterResults.All(e => e.Success), encounterResults);
        }
    }
}
