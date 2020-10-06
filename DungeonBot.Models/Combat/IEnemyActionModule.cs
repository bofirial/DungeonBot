namespace DungeonBot.Models.Combat
{
    public interface IEnemyActionModule
    {
        IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent);
    }
}
