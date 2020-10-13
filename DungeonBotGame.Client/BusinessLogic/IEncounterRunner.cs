﻿using System.Threading.Tasks;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.Models.Display;

namespace DungeonBotGame.Client.BusinessLogic
{
    public interface IEncounterRunner
    {
        Task<EncounterResult> RunDungeonEncounterAsync(DungeonBot dungeonBot, Encounter encounter);

        bool EncounterHasCompleted(DungeonBot dungeonBot, Enemy enemy, int roundCounter);
    }
}
