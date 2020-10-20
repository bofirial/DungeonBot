using System.Collections.Generic;

namespace DungeonBotGame
{
    public interface ISensorComponent
    {
        public IDungeonBot DungeonBot { get; }

        public IEnemy Enemy { get; }

        public int RoundCounter { get; }

        public IEnumerable<IEncounterRoundResult> EncounterRoundHistory { get; }
    }
}
