namespace DungeonBotGame.Models.ViewModels
{
    public record EncounterViewModel(string Name, int Order, string Description, string ProfileImageLocation, EnemyType EnemyType);
}
