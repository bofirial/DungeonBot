﻿using System.Collections.Generic;

namespace DungeonBotGame.Models.Combat
{
    public class SensorComponent : ISensorComponent
    {
        public SensorComponent(IDungeonBot dungeonBot, IEnemy enemy, int roundCounter, IEnumerable<IEncounterRoundResult> encounterRoundHistory)
        {
            DungeonBot = dungeonBot;
            Enemy = enemy;
            RoundCounter = roundCounter;
            EncounterRoundHistory = encounterRoundHistory;
        }

        public IDungeonBot DungeonBot { get; }

        public IEnemy Enemy { get; }

        public int RoundCounter { get; }

        public IEnumerable<IEncounterRoundResult> EncounterRoundHistory { get; }
    }
}