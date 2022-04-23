using System.Collections.Immutable;

namespace DungeonBotGame.Store.Adventures;

public record AdventureMapViewModel(
    Location MaxDimensions,
    IImmutableList<Location> DungeonBotSpawnLocations,
    IImmutableList<ImpassableLocation> ImpassableLocations,
    IImmutableList<EnemyTemplate> Enemies,
    IImmutableList<TreasureTemplate> Treasures);
