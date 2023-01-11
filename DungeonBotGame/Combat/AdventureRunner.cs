using System.Collections.Immutable;

namespace DungeonBotGame.Combat;

public interface IAdventureRunner
{
    Task<AdventureHistory> RunAdventureAsync(Data.Adventure adventure, IEnumerable<Data.DungeonBot> dungeonBotParty);
}

public class AdventureRunner : IAdventureRunner
{
    public Task<AdventureHistory> RunAdventureAsync(Data.Adventure adventure, IEnumerable<Data.DungeonBot> dungeonBotParty)
    {
        //TODO: Build AdventureContext with Combat models for Adventure, Adventure Map, DungeonBots, Enemies, etc.
        //TODO: Run each turn of the adventure

        var fakeAdventureHistory = CreateFakeTreasureHuntAdventureHistory();

        //TODO: Update state with the adventure result before returning

        return Task.FromResult(fakeAdventureHistory);
    }

    private static AdventureHistory CreateFakeTreasureHuntAdventureHistory()
    {
        var mapDimensions = new Location(7, 7);

        var backgroundLocations = new List<BackgroundLocation>()
        {
            new BackgroundLocation(new Location(2, 1), "images/blank.png"),
            new BackgroundLocation(new Location(3, 1), "images/blank.png"),
            new BackgroundLocation(new Location(4, 1), "images/blank.png"),
            new BackgroundLocation(new Location(1, 2), "images/blank.png"),
            new BackgroundLocation(new Location(2, 2), "images/blank.png"),
            new BackgroundLocation(new Location(3, 2), "images/blank.png"),
            new BackgroundLocation(new Location(4, 2), "images/blank.png"),
            new BackgroundLocation(new Location(5, 2), "images/blank.png"),
            new BackgroundLocation(new Location(1, 3), "images/blank.png"),
            new BackgroundLocation(new Location(2, 3), "images/blank.png"),
            new BackgroundLocation(new Location(3, 3), "images/blank.png"),
            new BackgroundLocation(new Location(4, 3), "images/blank.png"),
            new BackgroundLocation(new Location(5, 3), "images/blank.png"),
            new BackgroundLocation(new Location(1, 4), "images/blank.png"),
            new BackgroundLocation(new Location(2, 4), "images/blank.png"),
            new BackgroundLocation(new Location(3, 4), "images/blank.png"),
            new BackgroundLocation(new Location(4, 4), "images/blank.png"),
            new BackgroundLocation(new Location(5, 4), "images/blank.png"),
            new BackgroundLocation(new Location(2, 5), "images/blank.png"),
            new BackgroundLocation(new Location(3, 5), "images/blank.png"),
            new BackgroundLocation(new Location(4, 5), "images/blank.png"),

            new BackgroundLocation(new Location(0, 2), "images/wall.png", IsBarrier: true),
            new BackgroundLocation(new Location(1, 1), "images/wall.png", IsBarrier: true),
            new BackgroundLocation(new Location(2, 0), "images/wall.png", IsBarrier: true),
            new BackgroundLocation(new Location(0, 3), "images/wall.png", IsBarrier: true),
            new BackgroundLocation(new Location(0, 4), "images/wall.png", IsBarrier: true),
            new BackgroundLocation(new Location(1, 5), "images/wall.png", IsBarrier: true),
            new BackgroundLocation(new Location(2, 6), "images/wall.png", IsBarrier: true),
            new BackgroundLocation(new Location(3, 6), "images/wall.png", IsBarrier: true),
            new BackgroundLocation(new Location(4, 6), "images/wall.png", IsBarrier: true),
            new BackgroundLocation(new Location(5, 5), "images/wall.png", IsBarrier: true),
            new BackgroundLocation(new Location(6, 4), "images/wall.png", IsBarrier: true),
            new BackgroundLocation(new Location(6, 3), "images/wall.png", IsBarrier: true),
            new BackgroundLocation(new Location(6, 2), "images/wall.png", IsBarrier: true),
            new BackgroundLocation(new Location(5, 1), "images/wall.png", IsBarrier: true),
            new BackgroundLocation(new Location(4, 0), "images/wall.png", IsBarrier: true),
            new BackgroundLocation(new Location(3, 0), "images/wall.png", IsBarrier: true)
        }.ToImmutableList();

        var dungeonBotId = Guid.NewGuid().ToString();
        var treasureId = Guid.NewGuid().ToString();
        var adventureExitId = Guid.NewGuid().ToString();

        var treasure = new TreasureChest(treasureId, "images/treasure.png", "images/treasure-looted.png", new Location(3, 4), false);
        var adventureExit = new AdventureExit(adventureExitId, "images/entrance.png", new Location(3, 0));

        var dungeonBot = new DungeonBot(dungeonBotId, "images/dungeonbot.png", new Location(3, 1), "DungeonBot001", 10, 10);

        var fakeAdventureHistory = new AdventureHistory(
            new Dictionary<int, AdventureContext>() {
                { 1, new AdventureContext(new AdventureMap(mapDimensions, backgroundLocations, new List<ITarget>() { dungeonBot                                         , treasure                          , adventureExit }.ToImmutableList()), new AdventureStartAction()                                                            , new TimeSpan(0, 0, 0)) },
                { 2, new AdventureContext(new AdventureMap(mapDimensions, backgroundLocations, new List<ITarget>() { dungeonBot with { Location = new Location(3, 2)}   , treasure                          , adventureExit }.ToImmutableList()), new MoveAction(dungeonBot, new Location(3, 2))                                        , new TimeSpan(0, 0, 1)) },
                { 3, new AdventureContext(new AdventureMap(mapDimensions, backgroundLocations, new List<ITarget>() { dungeonBot with { Location = new Location(3, 3)}   , treasure                          , adventureExit }.ToImmutableList()), new MoveAction(dungeonBot with { Location = new Location(3, 2)}, new Location(3, 3))  , new TimeSpan(0, 0, 2)) },
                { 4, new AdventureContext(new AdventureMap(mapDimensions, backgroundLocations, new List<ITarget>() { dungeonBot with { Location = new Location(3, 3)}   , treasure with { IsLooted = true } , adventureExit }.ToImmutableList()), new InteractAction(dungeonBot with { Location = new Location(3, 3)}, treasure)        , new TimeSpan(0, 0, 3)) },
                { 5, new AdventureContext(new AdventureMap(mapDimensions, backgroundLocations, new List<ITarget>() { dungeonBot with { Location = new Location(3, 2)}   , treasure with { IsLooted = true } , adventureExit }.ToImmutableList()), new MoveAction(dungeonBot with { Location = new Location(3, 3)}, new Location(3, 2))  , new TimeSpan(0, 0, 4)) },
                { 6, new AdventureContext(new AdventureMap(mapDimensions, backgroundLocations, new List<ITarget>() { dungeonBot with { Location = new Location(3, 1)}   , treasure with { IsLooted = true } , adventureExit }.ToImmutableList()), new MoveAction(dungeonBot with { Location = new Location(3, 2)}, new Location(3, 1))  , new TimeSpan(0, 0, 5)) },
                { 7, new AdventureContext(new AdventureMap(mapDimensions, backgroundLocations, new List<ITarget>() { dungeonBot with { Location = new Location(3, 1)}   , treasure with { IsLooted = true } , adventureExit }.ToImmutableList()), new InteractAction(dungeonBot with { Location = new Location(3, 1)}, adventureExit)   , new TimeSpan(0, 0, 6)) }
            }.ToImmutableDictionary(),
        AdventureResultStatus.Success);

        return fakeAdventureHistory;
    }
}
