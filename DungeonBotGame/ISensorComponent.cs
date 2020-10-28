using System.Collections.Generic;

namespace DungeonBotGame
{
    public interface ISensorComponent
    {
        public IDungeonBot DungeonBot { get; }

        public IEnemy Enemy { get; }

        public int Round { get; }

        public IEnumerable<IEncounterRoundResult> EncounterRoundHistory { get; }
    }
}
