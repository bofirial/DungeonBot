namespace DungeonBotGame.SourceGenerators.Foundation;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class DungeonBotAttribute : Attribute
{
    public DungeonBotAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}
