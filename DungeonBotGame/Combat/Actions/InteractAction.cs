namespace DungeonBotGame.Combat;
public record InteractAction : IAction
{
    public InteractAction(string characterId, string interactTargetId)
    {
        CharacterId = characterId;
        InteractTargetId = interactTargetId;
    }

    public ActionType ActionType => ActionType.Interact;

    public string CharacterId { get; init; }

    public string InteractTargetId { get; init; }
}

