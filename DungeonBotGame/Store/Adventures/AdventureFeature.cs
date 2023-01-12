using System.Collections.Immutable;
using DungeonBotGame.Data;
using Fluxor;

namespace DungeonBotGame.Store.Adventures;
public class AdventureFeature : Feature<AdventureState>
{
    public override string GetName() => nameof(AdventureState);
    protected override AdventureState GetInitialState() => new(ImmutableList.Create(FirstTreasureAdventure));

    public readonly static Adventure FirstTreasureAdventure = new(
        Guid.NewGuid().ToString(),
        "A Bot's First Adventure",
        "Enter the abandoned cave, find the treasure, and escape through the front door.",
        new AdventureMap(
            MaxDimensions: new Location(7, 7),
            DungeonBotStartLocations: ImmutableList.Create(new Location(3, 1)),
            AdventureExitLocations: ImmutableList.Create(new AdventureExitLocation(new Location(3, 0), "images/entrance.png")),
            BackgroundLocations: ImmutableList.Create(
                new BackgroundLocation(new Location(2, 1), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(3, 1), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(4, 1), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(1, 2), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(2, 2), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(3, 2), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(4, 2), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(5, 2), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(1, 3), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(2, 3), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(3, 3), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(4, 3), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(5, 3), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(1, 4), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(2, 4), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(3, 4), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(4, 4), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(5, 4), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(2, 5), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(3, 5), "images/blank.png", IsBarrier: false),
                new BackgroundLocation(new Location(4, 5), "images/blank.png", IsBarrier: false),

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
            ),
            Enemies: ImmutableList<EnemyTemplate>.Empty,
            Treasures: ImmutableList.Create(new TreasureTemplate(new Location(3, 4), "images/treasure.png", "images/treasure-looted.png"))
            ),
        ImmutableList<AdventureResult>.Empty);
}
