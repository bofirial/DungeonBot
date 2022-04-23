using System.Collections.Immutable;

namespace DungeonBotGame.Foundation;

public record DungeonBotViewModel(string Name, IImmutableList<string> TargettedAbilities, IImmutableList<string> NonTargettedAbilities);
