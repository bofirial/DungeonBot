using System.Collections.Generic;

namespace DungeonBotGame.Models.Combat
{
    public class EncounterRoundResult : IEncounterRoundResult
    {
        public IEnumerable<IActionResult> ActionResults { get; set; }

        public int Round { get; set; }

        public int DungeonBotCurrentHealth { get; set; }

        public int EnemyCurrentHealth { get; set; }
    }
}
