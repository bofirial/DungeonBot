using System.Collections.Generic;
using DungeonBotGame.Models.ViewModels;
using Fluxor;

namespace DungeonBotGame.Client.Store.Adventures
{
    public class AdventureResultActionReducer : Reducer<AdventureState, AdventureResultAction>
    {
        public override AdventureState Reduce(AdventureState state, AdventureResultAction action)
        {
            var adventures = new List<AdventureViewModel>();

            foreach (var adventure in state.Adventures)
            {
                if (adventure == action.Adventure)
                {
                    var adventureResults = new List<AdventureResultViewModel>();

                    if (adventure.AdventureResults != null)
                    {
                        adventureResults.AddRange(adventure.AdventureResults);
                    }

                    adventureResults.Add(action.AdventureResult);

                    adventures.Add(new AdventureViewModel(adventure.Name, adventure.Description, adventure.Encounters, adventure.Status, adventureResults));
                }
                else
                {
                    adventures.Add(adventure);
                }
            }

            return new AdventureState(adventures);
        }
    }
}
