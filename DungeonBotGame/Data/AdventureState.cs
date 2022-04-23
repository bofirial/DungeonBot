using System.Collections.Immutable;

namespace DungeonBotGame.Store.Adventures;
public record AdventureState(IImmutableList<AdventureViewModel> Adventures);
