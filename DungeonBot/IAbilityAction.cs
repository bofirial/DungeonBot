namespace DungeonBot
{
    public interface IAbilityAction : IAction
    {
        public AbilityType AbilityType { get; }
    }
}
