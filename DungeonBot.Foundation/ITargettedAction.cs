namespace DungeonBotGame.Foundation;

public interface ITargettedAction : IAction
{
    ITarget Target { get; }
}
