using System.Threading.Tasks;
using DungeonBot.Models.Combat;

namespace DungeonBot.Client.BusinessLogic
{
    public interface IEncounterRoundRunner
    {
        Task RunEncounterRoundAsync(Player dungeonBot, Enemy enemy);
    }
}
