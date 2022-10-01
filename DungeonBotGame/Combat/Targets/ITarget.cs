namespace DungeonBotGame.Combat;

public interface ITarget
{
    public string Id { get; }

    public string ImagePath { get; }

    public Location Location { get; }
}
