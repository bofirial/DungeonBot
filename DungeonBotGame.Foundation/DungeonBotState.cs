using System.Collections.Immutable;

namespace DungeonBotGame.Foundation;

public record DungeonBotState(
    string Id,
    string Name,
    DungeonBotClass Class,
    short Level,
    short AvailableStatPoints,
    short Power,
    short Armor,
    short Speed,
    string ImageLocation,
    IImmutableList<AbilityType> Abilities);
