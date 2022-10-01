namespace DungeonBotGame.Combat;
public record class DungeonBot : IDungeonBot
{
    public DungeonBot(string id, string imagePath, Location location, string name, int maximumHealth, int currentHealth)
    {
        Id = id;
        ImagePath = imagePath;
        Location = location;
        Name = name;
        MaximumHealth = maximumHealth;
        CurrentHealth = currentHealth;
    }

    public string Id { get; init; }

    public string ImagePath { get; init; }

    public Location Location { get; init; }

    public string Name { get; init; }

    public int MaximumHealth { get; init; }

    public int CurrentHealth { get; init; }
}
