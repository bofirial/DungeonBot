using System.Collections.Generic;
using DungeonBot.Models.Combat;

namespace DungeonBot.Models.Display
{
    public class EncounterResult
    {
        public bool Success { get; set; }

        public IEnumerable<EncounterRoundResult> EncounterRoundResults { get; set; }

        public string ResultDisplayText { get; set; }
    }
}
