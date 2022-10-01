namespace DungeonBotGame.Combat;

public interface ITargettedAction : IAction
{
    ITarget Target { get; }
}
