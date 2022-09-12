using System.Collections.Immutable;

namespace DungeonBotGame.Data;

public record Adventure(string Id, string Name, string Description, AdventureMap AdventureMap, IImmutableList<AdventureResult> AdventureResults);
