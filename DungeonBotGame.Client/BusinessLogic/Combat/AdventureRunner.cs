using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DungeonBotGame.Client.BusinessLogic.Compilation;
using DungeonBotGame.Client.Store.Adventures;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface IAdventureRunner
    {
        Task<AdventureResultViewModel> RunAdventureAsync(RunAdventureAction runAdventureAction);
    }

    public class AdventureRunner : IAdventureRunner
    {
        private readonly IActionModuleContextProvider _actionModuleContextProvider;
        private readonly IEncounterRunner _encounterRunner;

        public AdventureRunner(IActionModuleContextProvider actionModuleContextProvider, IEncounterRunner encounterRunner)
        {
            _actionModuleContextProvider = actionModuleContextProvider;
            _encounterRunner = encounterRunner;
        }

        public async Task<AdventureResultViewModel> RunAdventureAsync(RunAdventureAction runAdventureAction)
        {
            var actionModuleContext = await _actionModuleContextProvider.GetActionModuleContext(runAdventureAction.ActionModuleLibrary);

            var dungeonBot = new DungeonBot(runAdventureAction.ActionModuleLibrary.Name, 100, actionModuleContext,
                new Dictionary<AbilityType, AbilityContext>() {
                    { AbilityType.HeavySwing, new AbilityContext() { MaximumCooldownRounds = 1 }}
                });

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
