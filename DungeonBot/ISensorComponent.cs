using System.Collections.Generic;

namespace DungeonBot
{
    public interface ISensorComponent
    {
        public IPlayer DungeonBot { get; }

        public IEnemy Enemy { get; }

        public int RoundCounter { get; }

        public IEnumerable<IEncounterRoundResult> EncounterRoundHistory { get; }
    }
}
