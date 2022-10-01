namespace DungeonBotGame.Combat;
public record MoveAction : IAction
{
    public MoveAction(string characterId, Location location)
    {
        CharacterId = characterId;
        Location = location;
    }

    public ActionType ActionType => ActionType.Move;

    public string CharacterId { get; init; }

    public Location Location { get; init; }
}
