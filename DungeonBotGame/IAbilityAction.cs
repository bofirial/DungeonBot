namespace DungeonBotGame
{
    public interface IAbilityAction : IAction
    {
        public AbilityType AbilityType { get; }
    }
}
