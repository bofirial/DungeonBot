namespace DungeonBot
{
    public interface IActionComponent
    {
        public ITargettedAction Attack(IEnemy targetEnemy);

        public ITargettedAbilityAction UseAbility(ITarget target, AbilityType abilityType);
    }
}
