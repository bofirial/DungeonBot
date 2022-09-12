using System.Collections.Immutable;
using Fluxor;

namespace DungeonBotGame.Store.Adventures;
public class AdventureFeature : Feature<AdventureState>
{
    public override string GetName() => nameof(AdventureState);
    protected override AdventureState GetInitialState() => new(ImmutableList.Create(FirstTreasureAdventure));

    public readonly static AdventureViewModel FirstTreasureAdventure = new(
        Guid.NewGuid().ToString(),
        "A Bot's First Adventure",
        "Enter the abandoned cave, find the treasure, and escape through the front door.",
        new AdventureMapViewModel(
            new Location(7, 7),
            ImmutableList.Create(new Location(3, 1)),
            ImmutableList.Create(
                new ImpassableLocation(new Location(0, 2), "images/wall.png"),
                new ImpassableLocation(new Location(1, 1), "images/wall.png"),
                new ImpassableLocation(new Location(2, 0), "images/wall.png"),
                new ImpassableLocation(new Location(0, 3), "images/wall.png"),
                new ImpassableLocation(new Location(0, 4), "images/wall.png"),
                new ImpassableLocation(new Location(1, 5), "images/wall.png"),
                new ImpassableLocation(new Location(2, 6), "images/wall.png"),
                new ImpassableLocation(new Location(3, 6), "images/wall.png"),
                new ImpassableLocation(new Location(4, 6), "images/wall.png"),
                new ImpassableLocation(new Location(5, 5), "images/wall.png"),
                new ImpassableLocation(new Location(6, 4), "images/wall.png"),
                new ImpassableLocation(new Location(6, 3), "images/wall.png"),
                new ImpassableLocation(new Location(6, 2), "images/wall.png"),
                new ImpassableLocation(new Location(5, 1), "images/wall.png"),
                new ImpassableLocation(new Location(4, 0), "images/wall.png"),
                new ImpassableLocation(new Location(3, 0), "images/entrance.png")
            ),
            ImmutableList<EnemyTemplate>.Empty,
            ImmutableList.Create(new TreasureTemplate(new Location(3, 4), "images/treasure.png"))
            ),
        ImmutableList<AdventureResultViewModel>.Empty);
}
