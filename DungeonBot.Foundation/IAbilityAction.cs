namespace DungeonBotGame.Foundation;

public interface IAbilityAction : IAction
{
    public AbilityType AbilityType { get; }
}
