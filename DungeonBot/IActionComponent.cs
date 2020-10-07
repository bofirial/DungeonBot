namespace DungeonBot
{
    public interface IActionComponent
    {
        public ITargettedAction Attack(ITarget attackTarget);

        public ITargettedAbilityAction UseAbility(ITarget target, AbilityType abilityType);

        public bool AbilityIsAvailable(AbilityType abilityType);
    }
}
