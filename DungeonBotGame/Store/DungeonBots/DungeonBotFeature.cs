using System.Collections.Immutable;
using DungeonBotGame.Data;
using Fluxor;

namespace DungeonBotGame.Store.DungeonBots;
public class DungeonBotFeature : Feature<DungeonBotState>
{
    public override string GetName() => nameof(DungeonBotState);
    //TODO: Should this make the default DungeonBotState instead?
    protected override DungeonBotState GetInitialState() => new DungeonBotState(ImmutableList<DungeonBotViewModel>.Empty);
}
