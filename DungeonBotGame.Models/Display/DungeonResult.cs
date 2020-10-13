using System.Collections.Generic;

namespace DungeonBotGame.Models.Display
{
    public class DungeonResult
    {
        public string RunId { get; set; }

        public bool Success { get; set; }

        public IEnumerable<EncounterResult> EncounterResults { get; set; }
    }
}
