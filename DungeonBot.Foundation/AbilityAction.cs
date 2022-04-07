namespace DungeonBotGame.Foundation;

public record AbilityAction : IAbilityAction
{
    public AbilityAction(AbilityType abilityType)
    {
        AbilityType = abilityType;
    }

    public AbilityType AbilityType { get; init; }

    public ActionType ActionType => ActionType.Ability;
}
