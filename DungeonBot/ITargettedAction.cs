namespace DungeonBot
{
    public interface ITargettedAction : IAction
    {
        ITarget Target { get; }
    }
}
