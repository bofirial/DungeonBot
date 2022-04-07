using DungeonBotGame.Foundation;

namespace DungeonBotGame.SampleGame.DungeonBots;

[DungeonBot("WarriorBot001")]
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
