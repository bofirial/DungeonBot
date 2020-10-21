using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public class SaveDungeonBotAction
    {
        public DungeonBotViewModel DungeonBot { get; }

        public string Code { get; }

        public SaveDungeonBotAction(DungeonBotViewModel dungeonBot, string code)
        {
            DungeonBot = dungeonBot;
            Code = code;
        }
    }
}
