namespace DungeonBotGame.Combat;

public interface ISensorComponent
{
    public IEnumerable<IDungeonBot> DungeonBots { get; }

    public IEnumerable<IEnemy> Enemies { get; }

    public TimeSpan CombatTime { get; }
}
