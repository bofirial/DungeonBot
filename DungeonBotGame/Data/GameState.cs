using DungeonBotGame.Store.Adventures;

namespace DungeonBotGame.Data;

public record GameState(DungeonBotState DungeonBotState, AdventureState AdventureState);
