using DungeonBotGame.Data;

namespace DungeonBotGame.Combat;

public record TargettedAbilityAction : ITargettedAbilityAction
{
    public TargettedAbilityAction(ICharacter character, ITarget target, AbilityType abilityType)
    {
        Character = character;
        Target = target;
        AbilityType = abilityType;
    }
    public ICharacter Character { get; init; }

    public AbilityType AbilityType { get; init; }

    public ITarget Target { get; init; }

    public ActionType ActionType => ActionType.Ability;
}
