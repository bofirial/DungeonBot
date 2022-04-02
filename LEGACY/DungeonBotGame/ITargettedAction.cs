namespace DungeonBotGame
{
    public interface ITargettedAction : IAction
    {
        ITarget Target { get; }
    }
}
