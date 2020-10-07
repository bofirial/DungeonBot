using System.Collections.Generic;

namespace DungeonBot
{
    public interface IEncounterRoundResult
    {
        public int Round { get; }

        public IEnumerable<IActionResult> ActionResults { get; }
    }
}
