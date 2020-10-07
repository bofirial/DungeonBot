using System.Threading.Tasks;
using DungeonBot.Models.Combat;
using DungeonBot.Models.Display;

namespace DungeonBot.Client.BusinessLogic
{
    public interface IEncounterRunner
    {
        Task<EncounterResult> RunDungeonEncounterAsync(Player dungeonBot, Encounter encounter);

        bool EncounterHasCompleted(Player dungeonBot, Enemy enemy, int roundCounter);
    }
}
