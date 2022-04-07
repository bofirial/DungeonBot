using System.Collections.Immutable;
using System.ComponentModel;
using DungeonBotGame.Foundation;

namespace DungeonBotGame.SampleGame.DungeonBots;

public partial class WarriorBot
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    private ImmutableDictionary<AbilityType, AbilityContext> Abilities { get; }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool AbilityIsAvailable(AbilityType abilityType) => Abilities.ContainsKey(abilityType) && Abilities[abilityType].IsAvailable;
    [EditorBrowsable(EditorBrowsableState.Never)]
    public IAbilityAction UseAbility(AbilityType abilityType) => new AbilityAction(abilityType);
    [EditorBrowsable(EditorBrowsableState.Never)]
    public ITargettedAbilityAction UseTargettedAbility(ITarget target, AbilityType abilityType) => new TargettedAbilityAction(target, abilityType);

    public ITargettedAction Attack(ITarget attackTarget) => new AttackAction(attackTarget);

    public bool HeavySwingIsAvailable() => AbilityIsAvailable(AbilityType.HeavySwing);
    public ITargettedAbilityAction UseHeavySwing(ITarget target) => UseTargettedAbility(target, AbilityType.HeavySwing);
}
