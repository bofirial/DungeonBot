using System.Collections.Immutable;

namespace DungeonBotGame.Combat;

public record AdventureMap(Location MaxDimensions, IImmutableList<ImpassableLocation> ImpassableLocations, IImmutableList<ITarget> TargetLocations);
