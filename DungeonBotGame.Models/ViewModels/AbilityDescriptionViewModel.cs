namespace DungeonBotGame.Models.ViewModels
{
    public record AbilityDescriptionViewModel(string Name, string Description, AbilityType AbilityType, bool IsTargettedAbility, int CooldownCombatTime);
}
