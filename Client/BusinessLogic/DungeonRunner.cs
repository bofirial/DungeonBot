using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DungeonBot.Client.Store.Dungeons;
using DungeonBot.Models.Combat;
using DungeonBot.Models.Display;

namespace DungeonBot.Client.BusinessLogic
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

        public async Task<DungeonResult> RunDungeonAsync(RunDungeonAction runDungeonAction)
        {
            var actionModuleContext = await _actionModuleContextProvider.GetActionModuleContext(runDungeonAction.ActionModuleLibrary);

            var dungeonBot = new Player(runDungeonAction.ActionModuleLibrary.Name, 100, actionModuleContext);

            var encounterResults = new List<EncounterResult>();

            foreach (var encounter in runDungeonAction.Dungeon.Encounters)
            {
                var encounterResult = await _encounterRunner.RunDungeonEncounterAsync(dungeonBot, encounter);

                encounterResults.Add(encounterResult);

                if (!encounterResult.Success)
                {
                    return new DungeonResult()
                    {
                        RunId = runDungeonAction.RunId,
                        Success = false,
                        EncounterResults = encounterResults
                    };
                }
            }

            return new DungeonResult()
            {
                RunId = runDungeonAction.RunId,
                Success = encounterResults.All(e => e.Success),
                EncounterResults = encounterResults
            };
        }
    }
}
