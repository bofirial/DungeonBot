namespace DungeonBotGame.Combat;
public record MoveAction : IAction
{
    public MoveAction(ICharacter character, Location location)
    {
        Character = character;
        Location = location;
    }

    public ActionType ActionType => ActionType.Move;

    public ICharacter Character { get; init; }

    public Location Location { get; init; }
}
