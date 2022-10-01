namespace DungeonBotGame.Combat;

public record TreasureChest : ITarget
{
    public TreasureChest(string id, string treasureImagePath, string lootedTreasureImagePath, Location location, bool isLooted)
    {
        Id = id;
        TreasureImagePath = treasureImagePath;
        LootedTreasureImagePath = lootedTreasureImagePath;
        Location = location;
        IsLooted = isLooted;
    }

    public string Id { get; init; }

    public string ImagePath => IsLooted ? LootedTreasureImagePath : TreasureImagePath;

    public Location Location { get; init; }

    public bool IsLooted { get; init; }

    public string TreasureImagePath { get; init; }

    public string LootedTreasureImagePath { get; init; }
}
