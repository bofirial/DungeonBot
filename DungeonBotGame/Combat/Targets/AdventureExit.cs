namespace DungeonBotGame.Combat;
public record AdventureExit : ITarget
{
    public AdventureExit(string id, string imagePath, Location location)
    {
        Id = id;
        ImagePath = imagePath;
        Location = location;
    }

    public string Id { get; init; }

    public string ImagePath { get; init; }

    public Location Location { get; init; }
}
