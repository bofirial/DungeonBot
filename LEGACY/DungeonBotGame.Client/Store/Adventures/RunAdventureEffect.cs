using System.Threading.Tasks;
using DungeonBotGame.Client.BusinessLogic.Combat;
using Fluxor;

namespace DungeonBotGame.Client.Store.Adventures
{
    public class RunAdventureEffect : Effect<RunAdventureAction>
    {
        private readonly IAdventureRunner _adventureRunner;

        public RunAdventureEffect(IAdventureRunner adventureRunner)
        {
            _adventureRunner = adventureRunner;
        }

        protected override async Task HandleAsync(RunAdventureAction action, IDispatcher dispatcher)
        {
            var adventureResult = await _adventureRunner.RunAdventureAsync(action);

            dispatcher.Dispatch(new AdventureResultAction(action.Adventure, adventureResult));
        }
    }
}
