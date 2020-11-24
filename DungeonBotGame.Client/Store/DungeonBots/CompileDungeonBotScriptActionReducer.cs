using Fluxor;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public class CompileDungeonBotScriptActionReducer : Reducer<DungeonBotState, CompileDungeonBotScriptAction>
    {
        public override DungeonBotState Reduce(DungeonBotState state, CompileDungeonBotScriptAction action)
        {
            return state with
            {
                IsSaving = true
            };
        }
    }
}
