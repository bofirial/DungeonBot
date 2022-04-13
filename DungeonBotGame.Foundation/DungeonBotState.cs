using System.Collections.Immutable;

namespace DungeonBotGame.Foundation;

public record DungeonBotState(
    string Name,
    IImmutableList<string> TargettedAbilities,
    IImmutableList<string> NonTargettedAbilities);
