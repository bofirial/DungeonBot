using System.Collections.Generic;

namespace DungeonBotGame
{
    public interface ISensorComponent
    {
        public IEnumerable<IDungeonBot> DungeonBots { get; }

        public IEnumerable<IEnemy> Enemies { get; }

        public int CombatTime { get; }

        public IEnumerable<IActionResult> ActionResults { get; }
    }
}
