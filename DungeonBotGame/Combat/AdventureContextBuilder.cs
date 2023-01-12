using System.Collections.Immutable;
using DungeonBotGame.Data;

namespace DungeonBotGame.Combat;

public interface IAdventureContextBuilder
{
    AdventureContext CreateAdventureContext(Adventure adventure, IEnumerable<Data.DungeonBot> dungeonBotParty);
}

public class AdventureContextBuilder : IAdventureContextBuilder
{
    public AdventureContext CreateAdventureContext(Adventure adventure, IEnumerable<Data.DungeonBot> dungeonBotParty)
    {
        var adventureMap = new AdventureMap(
                MapLocation(adventure.AdventureMap.MaxDimensions),
                adventure.AdventureMap.BackgroundLocations.Select(MapBackgroundLocation).ToImmutableList(),
                CreateTargets(adventure, dungeonBotParty).ToImmutableList());

        return new AdventureContext(adventureMap, new AdventureStartAction(), new TimeSpan(0, 0, 0));
    }

    private static Location MapLocation(Data.Location location) => new Location(location.X, location.Y);
    private static BackgroundLocation MapBackgroundLocation(Data.BackgroundLocation l) => new BackgroundLocation(MapLocation(l.Location), l.ImagePath, l.IsBarrier);
    private static IEnumerable<ITarget> CreateTargets(Adventure adventure, IEnumerable<Data.DungeonBot> dungeonBotParty)
    {
        return CreateDungeonBotTargets(adventure.AdventureMap.DungeonBotStartLocations, dungeonBotParty)
                            .Concat(CreateEnemyTargets(adventure.AdventureMap.Enemies))
                            .Concat(CreateTreasureTargets(adventure.AdventureMap.Treasures))
                            .Concat(CreateAdventureExitTargets(adventure.AdventureMap.AdventureExitLocations));
    }
    private static IEnumerable<ITarget> CreateDungeonBotTargets(IImmutableList<Data.Location> dungeonBotStartLocations, IEnumerable<Data.DungeonBot> dungeonBotParty) => dungeonBotParty.Zip(dungeonBotStartLocations, MapDungeonBot);
    private static IEnumerable<ITarget> CreateEnemyTargets(IImmutableList<EnemyTemplate> enemies) => Array.Empty<ITarget>(); //TODO: Create Enemies enemies.Select(e => new Enemy());
    private static IEnumerable<ITarget> CreateTreasureTargets(IImmutableList<TreasureTemplate> treasures) => treasures.Select(MapTreasure);
    private static IEnumerable<ITarget> CreateAdventureExitTargets(IImmutableList<Data.AdventureExitLocation> adventureExitLocations) => adventureExitLocations.Select(a => new AdventureExit(Guid.NewGuid().ToString(), a.ImagePath, MapLocation(a.Location)));
    private static DungeonBot MapDungeonBot(Data.DungeonBot dungeonBot, Data.Location location) => new DungeonBot(Guid.NewGuid().ToString(), dungeonBot.ImagePath, MapLocation(location), dungeonBot.Name, dungeonBot.Armor * 10, dungeonBot.Armor * 10);
    private static TreasureChest MapTreasure(TreasureTemplate t) => new TreasureChest(Guid.NewGuid().ToString(), t.ImagePath, t.LootedImagePath, MapLocation(t.TreasureLocation), false);

}
