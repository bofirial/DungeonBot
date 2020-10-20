using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic
{
    public interface IAbilityDescriptionProvider
    {
        AbilityDescriptionViewModel GetAbilityDescription(AbilityType abilityType);
    }
}
