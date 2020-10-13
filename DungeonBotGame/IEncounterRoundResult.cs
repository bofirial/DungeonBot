using System.Collections.Generic;

namespace DungeonBotGame
{
    public interface IEncounterRoundResult
    {
        public int Round { get; }

        public IEnumerable<IActionResult> ActionResults { get; }
    }
}
