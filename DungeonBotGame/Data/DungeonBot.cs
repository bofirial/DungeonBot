using System.Collections.Immutable;

namespace DungeonBotGame.Data;

public record DungeonBot(
    string Id,
    string Name,
    DungeonBotClassification? Classification,
    short Level,
    short AvailableStatPoints,
    short Power,
    short Armor,
    short Speed,
    string ImagePath,
    IImmutableList<AbilityType> TargettedAbilities,
    IImmutableList<AbilityType> NonTargettedAbilities)
{
    public static readonly DungeonBot SelectADungeonBot = new DungeonBot(
        Guid.Empty.ToString(),
        "Select a Dungeonbot",
        default,
        default,
        default,
        default,
        default,
        default,
        "images/dungeonbot-select.png",
        ImmutableList<AbilityType>.Empty,
        ImmutableList<AbilityType>.Empty);
}
