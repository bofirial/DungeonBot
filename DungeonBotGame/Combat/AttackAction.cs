namespace DungeonBotGame.Combat;

public record AttackAction : ITargettedAction
{
    public AttackAction(ITarget attackTarget)
    {
        Target = attackTarget;
    }

    public ITarget Target { get; }

    public ActionType ActionType => ActionType.Attack;
}
