namespace DungeonBotGame.Combat;
public record AdventureStartAction : IAction
{
    public ActionType ActionType => ActionType.AdventureStart;
}
