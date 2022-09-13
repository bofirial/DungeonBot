
namespace DungeonBotGame.Data;
public interface IDungeonBotClassificationDisplayNameProvider
{
    string GetDisplayName(DungeonBotClassification? dungeonBotClassification);
}

public class DungeonBotClassificationDisplayNameProvider : IDungeonBotClassificationDisplayNameProvider
{
    public string GetDisplayName(DungeonBotClassification? dungeonBotClassification) => dungeonBotClassification switch
    {
        DungeonBotClassification.WarriorBot => "Warrior Bot",
        DungeonBotClassification.ArmorBot => "Armor Bot",
        DungeonBotClassification.SorcererBot => "Sorcerer Bot",
        DungeonBotClassification.MysticRepairBot => "Mystic Repair Bot",
        _ => string.Empty,
    };
}
