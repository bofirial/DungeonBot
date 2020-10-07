using System.Collections.Generic;

namespace DungeonBot.Models.Combat
{
    public class SensorComponent : ISensorComponent
    {
        public SensorComponent(IPlayer dungeonBot, IEnemy enemy, int roundCounter, IEnumerable<IEncounterRoundResult> encounterRoundHistory)
        {
            DungeonBot = dungeonBot;
            Enemy = enemy;
            RoundCounter = roundCounter;
            EncounterRoundHistory = encounterRoundHistory;
        }

        public IPlayer DungeonBot { get; }

        public IEnemy Enemy { get; }

        public int RoundCounter { get; }

        public IEnumerable<IEncounterRoundResult> EncounterRoundHistory { get; }
    }
}
