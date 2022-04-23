namespace DungeonBotGame.Combat;

public interface ISensorComponent
{
    public IEnumerable<IDungeonBot> DungeonBots { get; }

    public IEnumerable<IEnemy> Enemies { get; }

    public int CombatTime { get; }

    //public IEnumerable<ICombatLogEntry> CombatLog { get; }
}
