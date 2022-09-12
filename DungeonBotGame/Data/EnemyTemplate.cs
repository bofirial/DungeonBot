namespace DungeonBotGame.Data;

public record EnemyTemplate(
    Location EnemySpawnLocation,
    string Name,
    short Level,
    short Power,
    short Armor,
    short Speed,
    // How does this Enemy determine which Action to use? (ActionModule)
    string ImagePath);
