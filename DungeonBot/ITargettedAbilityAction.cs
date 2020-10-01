namespace DungeonBot
{
    public interface ITargettedAbilityAction : ITargettedAction
    {
        public AbilityType AbilityType { get; }
    }
}
