using System.Collections.Immutable;

namespace DungeonBotGame.Data;

public record DungeonBotState(IImmutableList<DungeonBot> DungeonBots);
