using DungeonBotGame.Foundation;

namespace DungeonBotGame.Combat;

public interface IAbilityAction : IAction
{
    public AbilityType AbilityType { get; }
}
