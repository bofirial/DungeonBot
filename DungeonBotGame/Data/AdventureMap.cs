using System.Collections.Immutable;

namespace DungeonBotGame.Data;

public record AdventureMap(
    Location MaxDimensions,
    IImmutableList<Location> DungeonBotSpawnLocations,
    IImmutableList<ImpassableLocation> ImpassableLocations,
    IImmutableList<EnemyTemplate> Enemies,
    IImmutableList<TreasureTemplate> Treasures);
