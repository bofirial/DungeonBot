namespace DungeonBotGame.Models.Combat
{
    public class TargettedAbilityAction : ITargettedAbilityAction
    {
        public TargettedAbilityAction(ITarget target, AbilityType abilityType)
        {
            Target = target;
            AbilityType = abilityType;
        }

        public AbilityType AbilityType { get; }

        public ITarget Target { get; }

        public ActionType ActionType => ActionType.Ability;
    }
}
