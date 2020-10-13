using System.Collections.Generic;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Models.Display
{
    public class EncounterResult
    {
        public bool Success { get; set; }

        public IEnumerable<EncounterRoundResult> EncounterRoundResults { get; set; }

        public string ResultDisplayText { get; set; }
    }
}
