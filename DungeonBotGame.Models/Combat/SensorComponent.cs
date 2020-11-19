using System.Collections.Generic;

namespace DungeonBotGame.Models.Combat
{
    public record SensorComponent(IEnumerable<IDungeonBot> DungeonBots, IEnumerable<IEnemy> Enemies, int CombatTime, IEnumerable<IActionResult> ActionResults) : ISensorComponent;
}
