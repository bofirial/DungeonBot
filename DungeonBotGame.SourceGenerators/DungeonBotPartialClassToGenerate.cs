namespace DungeonBotGame.SourceGenerators;

public readonly struct DungeonBotPartialClassToGenerate
{
    public readonly string Name;
    public readonly string Namespace;
    public readonly List<string> DungeonBotNames;

    public DungeonBotPartialClassToGenerate(string name, string @namespace, List<string> dungeonBotNames)
    {
        Name = name;
        Namespace = @namespace;
        DungeonBotNames = dungeonBotNames;
    }
}
