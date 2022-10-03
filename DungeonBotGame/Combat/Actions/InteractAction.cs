namespace DungeonBotGame.Combat;
public record InteractAction : ITargettedAction
{
    public InteractAction(ICharacter character, ITarget target)
    {
        Character = character;
        Target = target;
    }

    public ActionType ActionType => ActionType.Interact;

    public ICharacter Character { get; init; }

    public ITarget Target { get; init; }
}

