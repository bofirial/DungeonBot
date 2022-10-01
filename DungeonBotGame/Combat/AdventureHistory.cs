using System.Collections.Immutable;

namespace DungeonBotGame.Combat;

public record AdventureHistory(IImmutableDictionary<int, AdventureContext> TurnHistory, AdventureResultStatus AdventureResultStatus);
