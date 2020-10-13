using System.Collections.Generic;
using System.Threading.Tasks;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic
{
    public interface IEncounterRoundRunner
    {
        Task<EncounterRoundResult> RunEncounterRoundAsync(DungeonBot dungeonBot, Enemy enemy, int roundCounter, IEnumerable<EncounterRoundResult> encounterRoundResults);
    }
}
