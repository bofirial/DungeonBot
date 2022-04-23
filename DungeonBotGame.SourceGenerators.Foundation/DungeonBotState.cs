using System.Collections.Immutable;

namespace DungeonBotGame.SourceGenerators.Foundation;

public record DungeonBotState(IImmutableList<DungeonBotViewModel> DungeonBots);
