using System.Collections.Immutable;

namespace DungeonBotGame.Data;

public record DungeonBotViewModel(
    string Id,
    string Name,
    DungeonBotClass Class,
    short Level,
    short AvailableStatPoints,
    short Power,
    short Armor,
    short Speed,
    string ImageLocation,
    IImmutableList<AbilityType> TargettedAbilities,
    IImmutableList<AbilityType> NonTargettedAbilities);
