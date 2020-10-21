using System.ComponentModel;

namespace DungeonBotGame
{
    public interface IActionComponent
    {
        public ITargettedAction Attack(ITarget attackTarget);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IAbilityAction UseAbility(AbilityType abilityType);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public ITargettedAbilityAction UseTargettedAbility(ITarget target, AbilityType abilityType);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool AbilityIsAvailable(AbilityType abilityType);
    }
}
