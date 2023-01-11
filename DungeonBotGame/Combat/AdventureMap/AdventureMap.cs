using System.Collections.Immutable;

namespace DungeonBotGame.Combat;

public record AdventureMap(Location MaxDimensions, IImmutableList<BackgroundLocation> Locations, IImmutableList<ITarget> Targets);
