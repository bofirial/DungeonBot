namespace DungeonBotGame.Combat;

public record AttackAction : ITargettedAction
{
    public AttackAction(ICharacter character, ITarget attackTarget)
    {
        Character = character;
        Target = attackTarget;
    }
    public ICharacter Character { get; init; }

    public ITarget Target { get; }

    public ActionType ActionType => ActionType.Attack;
}
