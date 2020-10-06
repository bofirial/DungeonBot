namespace DungeonBot.Models.Combat
{
    public class ActionComponent : IActionComponent
    {
        public ITargettedAction Attack(ITarget attackTarget) => new AttackAction(attackTarget);

        public ITargettedAbilityAction UseAbility(ITarget target, AbilityType abilityType) => new TargettedAbilityAction(target, abilityType);
    }
}
