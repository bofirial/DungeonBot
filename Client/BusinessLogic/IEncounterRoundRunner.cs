using System.Collections.Generic;
using System.Threading.Tasks;
using DungeonBot.Models.Combat;

namespace DungeonBot.Client.BusinessLogic
{
    public interface IEncounterRoundRunner
    {
        Task<EncounterRoundResult> RunEncounterRoundAsync(Player dungeonBot, Enemy enemy, int roundCounter, IEnumerable<EncounterRoundResult> encounterRoundResults);
    }
}
