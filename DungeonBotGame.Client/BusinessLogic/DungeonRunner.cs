using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DungeonBotGame.Client.Store.Dungeons;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.Models.Display;

namespace DungeonBotGame.Client.BusinessLogic
{
    public class DungeonRunner : IDungeonRunner
    {
        private readonly IActionModuleContextProvider _actionModuleContextProvider;
        private readonly IEncounterRunner _encounterRunner;

        public DungeonRunner(IActionModuleContextProvider actionModuleContextProvider, IEncounterRunner encounterRunner)
        {
            _actionModuleContextProvider = actionModuleContextProvider;
            _encounterRunner = encounterRunner;
        }

        public async Task<DungeonResultViewModel> RunDungeonAsync(RunDungeonAction runDungeonAction)
        {
            var actionModuleContext = await _actionModuleContextProvider.GetActionModuleContext(runDungeonAction.ActionModuleLibrary);

            var dungeonBot = new DungeonBot(runDungeonAction.ActionModuleLibrary.Name, 100, actionModuleContext,
                new Dictionary<AbilityType, AbilityContext>() {
                    { AbilityType.HeavySwing, new AbilityContext() { MaximumCooldownRounds = 1 }}
                });

            var encounterResults = new List<EncounterResultViewModel>();

            foreach (var encounter in runDungeonAction.Dungeon.Encounters)
            {
                var encounterResult = await _encounterRunner.RunDungeonEncounterAsync(dungeonBot, encounter);

                encounterResults.Add(encounterResult);

                if (!encounterResult.Success)
                {
                    return new DungeonResultViewModel()
                    {
                        RunId = runDungeonAction.RunId,
                        Success = false,
                        EncounterResults = encounterResults
                    };
                }
            }

            return new DungeonResultViewModel()
            {
                RunId = runDungeonAction.RunId,
                Success = encounterResults.All(e => e.Success),
                EncounterResults = encounterResults
            };
        }
    }
}
