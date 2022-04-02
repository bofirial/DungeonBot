namespace DungeonBotGame.Models.ViewModels
{
    public record AbilityDescriptionViewModel(string Name, string Description, AbilityType AbilityType, bool IsTargetted, bool IsPassive, int CooldownCombatTime);
}
