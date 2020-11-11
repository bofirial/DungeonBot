using System.Collections.Generic;

namespace DungeonBotGame.Models.Combat
{
    public record SensorComponent(IDungeonBot DungeonBot, IEnemy Enemy, int Round, IEnumerable<IEncounterRoundResult> EncounterRoundHistory) : ISensorComponent;
}
