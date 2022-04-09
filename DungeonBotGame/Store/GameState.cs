using System.Collections.Immutable;

namespace DungeonBotGame.Store;
public record GameState(IImmutableList<DungeonBotState> DungeonBots);
