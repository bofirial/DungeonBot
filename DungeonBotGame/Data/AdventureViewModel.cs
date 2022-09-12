using System.Collections.Immutable;

namespace DungeonBotGame.Store.Adventures;

public record AdventureViewModel(string Id, string Name, string Description, AdventureMapViewModel AdventureMap, IImmutableList<AdventureResultViewModel> AdventureResults);
