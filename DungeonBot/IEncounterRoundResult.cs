using System.Collections.Generic;

namespace DungeonBot
{
    public interface IEncounterRoundResult
    {
        public IEnumerable<IActionResult> ActionResults { get; }
    }
}
