using System.Collections.Generic;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Models.ViewModels
{
    public class EncounterResultViewModel
    {
        public bool Success { get; set; }

        public IEnumerable<EncounterRoundResult> EncounterRoundResults { get; set; }

        public string ResultDisplayText { get; set; }
    }
}
