using System.Collections.Generic;

namespace DungeonBotGame.Models.ViewModels
{
    public class DungeonResultViewModel
    {
        public string RunId { get; set; }

        public bool Success { get; set; }

        public IEnumerable<EncounterResultViewModel> EncounterResults { get; set; }
    }
}
