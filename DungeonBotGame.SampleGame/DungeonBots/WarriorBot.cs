using System.ComponentModel;
using DungeonBotGame.Combat;
using DungeonBotGame.Foundation;

namespace DungeonBotGame.SampleGame.DungeonBots;

[DungeonBot("WarriorBot01")]
[EditorBrowsable(EditorBrowsableState.Never)]
[DungeonBot("WarriorBot02")]
public partial class WarriorBot
{
    public IAction Action(ISensorComponent sensorComponent)
    {
        var enemy = sensorComponent.Enemies.First();

        if (HeavySwingIsAvailable())
        {
            return UseHeavySwing(enemy);
        }

        return Attack(enemy);
    }
}
