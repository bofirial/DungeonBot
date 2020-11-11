namespace DungeonBotGame.Models.ViewModels
{
    public record AbilityDescriptionViewModel(string Name, string Description, AbilityType AbilityType, int CooldownRounds, bool IsTargettedAbility, int StartOfCombatCooldownRounds);
}
