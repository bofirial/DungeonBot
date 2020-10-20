namespace DungeonBotGame.Models.Combat
{
    public interface IEnemyActionModule
    {
        IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent);
    }
}
