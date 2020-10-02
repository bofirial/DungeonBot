namespace DungeonBot.Models
{
    public class ActionComponent : IActionComponent
    {
        public ITargettedAction Attack(IEnemy targetEnemy) => new AttackAction(targetEnemy);

        public ITargettedAbilityAction UseAbility(ITarget target, AbilityType abilityType) => new TargettedAbilityAction(target, abilityType);
    }
}
