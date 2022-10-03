using DungeonBotGame.Data;

namespace DungeonBotGame.Combat;

public record AbilityAction : IAbilityAction
{
    public AbilityAction(ICharacter character, AbilityType abilityType)
    {
        Character = character;
        AbilityType = abilityType;
    }
    public ICharacter Character { get; init; }

    public AbilityType AbilityType { get; init; }

    public ActionType ActionType => ActionType.Ability;
}
