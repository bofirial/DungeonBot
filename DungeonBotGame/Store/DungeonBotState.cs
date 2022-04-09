using System.Collections.Immutable;
using DungeonBotGame.Combat;

namespace DungeonBotGame.Store;

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
