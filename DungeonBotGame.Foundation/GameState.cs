using System.Collections.Immutable;

namespace DungeonBotGame.Foundation;

public record GameState(IImmutableList<DungeonBotState> DungeonBots);
