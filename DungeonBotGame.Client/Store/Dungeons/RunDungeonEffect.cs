using System.Threading.Tasks;
using DungeonBotGame.Client.BusinessLogic.Combat;
using Fluxor;

namespace DungeonBotGame.Client.Store.Dungeons
{
    public class RunDungeonEffect : Effect<RunDungeonAction>
    {
        private readonly IDungeonRunner _dungeonRunner;

        public RunDungeonEffect(IDungeonRunner dungeonRunner)
        {
            _dungeonRunner = dungeonRunner;
        }

        protected override async Task HandleAsync(RunDungeonAction action, IDispatcher dispatcher)
        {
            var dungeonResult = await _dungeonRunner.RunDungeonAsync(action);

            dispatcher.Dispatch(new DungeonResultAction(action.Dungeon, dungeonResult));
        }
    }
}
