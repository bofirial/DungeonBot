using DungeonBotGame.Client.ErrorHandling;

namespace DungeonBotGame.Client.BusinessLogic
{
    public interface IClassDetailProvider
    {
        string GetClassName(DungeonBotClass dungeonBotClass);
    }

    public class ClassDetailProvider : IClassDetailProvider
    {
        public string GetClassName(DungeonBotClass dungeonBotClass)
        {
            return dungeonBotClass switch
            {
                DungeonBotClass.WarriorBot => "Warrior Bot",
                DungeonBotClass.ArmorBot => "Armor Bot",
                DungeonBotClass.SorcererBot => "Sorcerer Bot",
                DungeonBotClass.MysticRepairBot => "Mystic Repair Bot",
                _ => throw new UnknownDungeonBotClassException(dungeonBotClass),
            };
        }
    }
}
