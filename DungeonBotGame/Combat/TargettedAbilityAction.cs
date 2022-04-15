using DungeonBotGame.Data;

namespace DungeonBotGame.Combat;

public record TargettedAbilityAction : ITargettedAbilityAction
{
    public TargettedAbilityAction(ITarget target, AbilityType abilityType)
    {
        Target = target;
        AbilityType = abilityType;
    }

    public AbilityType AbilityType { get; init; }

    public ITarget Target { get; init; }

    public ActionType ActionType => ActionType.Ability;
}
