using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public record SaveDungeonBotAction
    {
        public DungeonBotViewModel DungeonBot { get; init; }

        public string Code { get; init; }

        public SaveDungeonBotAction(DungeonBotViewModel dungeonBot, string code)
        {
            DungeonBot = dungeonBot;
            Code = code;
        }
    }
}
