using System.Threading.Tasks;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic
{
    public interface IEncounterRunner
    {
        Task<EncounterResultViewModel> RunDungeonEncounterAsync(DungeonBot dungeonBot, EncounterViewModel encounter);

        bool EncounterHasCompleted(DungeonBot dungeonBot, Enemy enemy, int roundCounter);
    }
}
