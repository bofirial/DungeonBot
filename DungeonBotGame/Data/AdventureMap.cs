using System.Collections.Immutable;

namespace DungeonBotGame.Data;

public record AdventureMap(
    Location MaxDimensions,
    IImmutableList<Location> DungeonBotStartLocations,
    IImmutableList<AdventureExitLocation> AdventureExitLocations,
    IImmutableList<BackgroundLocation> BackgroundLocations,
    IImmutableList<EnemyTemplate> Enemies,
    IImmutableList<TreasureTemplate> Treasures);
