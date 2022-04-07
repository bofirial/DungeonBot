namespace DungeonBotGame.Foundation;

[AttributeUsage(AttributeTargets.Class)]
public class DungeonBotAttribute : Attribute
{
    public DungeonBotAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}
