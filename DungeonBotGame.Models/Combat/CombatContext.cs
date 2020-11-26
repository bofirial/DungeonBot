using System.Collections.Generic;
using System.Collections.Immutable;

namespace DungeonBotGame.Models.Combat
{
    public class CombatContext
    {
        public int CombatTimer { get; set; }
        public List<CharacterBase> Characters { get; set; }
        public List<CombatLogEntry> CombatLog { get; set; }
        public List<CombatEvent> CombatEvents { get; set; }
        public List<CombatEvent> NewCombatEvents { get; set; }

        public IImmutableList<DungeonBot> DungeonBots { get; set; }
        public IImmutableList<Enemy> Enemies { get; set; }
    }
}
