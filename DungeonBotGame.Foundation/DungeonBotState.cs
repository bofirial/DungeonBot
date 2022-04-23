using System.Collections.Immutable;

namespace DungeonBotGame.Foundation;

public record DungeonBotState(IImmutableList<DungeonBotViewModel> DungeonBots);
