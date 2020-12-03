using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
            var dungeonBotViewModels = runAdventureAction.DungeonBots;
            var dungeonBots = new List<DungeonBot>();

            foreach (var actionDungeonBotViewModel in dungeonBotViewModels)
            {
                var dungeonBotViewModel = actionDungeonBotViewModel;

                if (dungeonBotViewModel.ActionModuleContext == null)
                {
                    dungeonBotViewModel = await _actionModuleContextBuilder.BuildActionModuleContextAsync(dungeonBotViewModel);

                    if (dungeonBotViewModel.Errors.Any())
                    {
                        //TODO: Handle Compilation Errors Gracefully #87
                        throw new Exception($"Compilation Error: {string.Join(Environment.NewLine, dungeonBotViewModel.Errors.Select(e => e.ErrorMessage))}");
                    }
                }

                dungeonBots.Add(_dungeonBotFactory.CreateCombatDungeonBot(dungeonBotViewModel));
            }

            var encounterResults = new List<EncounterResultViewModel>();

            foreach (var encounter in runAdventureAction.Adventure.Encounters)
            {
                var encounterResult = await _encounterRunner.RunAdventureEncounterAsync(dungeonBots.ToImmutableList(), encounter);

                encounterResults.Add(encounterResult);

                if (!encounterResult.Success)
                {
                    return new AdventureResultViewModel(runAdventureAction.RunId, Success: false, $"{(dungeonBots.Count > 1 ? "The DungeonBots have" : $"{dungeonBots[0].Name} has")} failed to complete the adventure.", encounterResults.ToImmutableList());
                }
            }

            return new AdventureResultViewModel(runAdventureAction.RunId, Success: encounterResults.All(e => e.Success), $"{(dungeonBots.Count > 1 ? "The DungeonBots have" : $"{dungeonBots[0].Name} has")} completed the adventure.", encounterResults.ToImmutableList());
        }
    }
}
