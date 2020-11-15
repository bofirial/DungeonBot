using System.Threading.Tasks;
using DungeonBotGame.Client.BusinessLogic.Compilation;
using Fluxor;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public class CompileDungeonBotScriptActionEffect : Effect<CompileDungeonBotScriptAction>
    {
        private readonly IActionModuleContextBuilder _actionModuleContextBuilder;

        public CompileDungeonBotScriptActionEffect(IActionModuleContextBuilder actionModuleContextBuilder)
        {
            _actionModuleContextBuilder = actionModuleContextBuilder;
        }

        protected override async Task HandleAsync(CompileDungeonBotScriptAction action, IDispatcher dispatcher)
        {
            var dungeonBot = await _actionModuleContextBuilder.BuildActionModuleContextAsync(action.DungeonBot);

            dispatcher.Dispatch(new SaveDungeonBotAction(dungeonBot));
        }
    }
}
