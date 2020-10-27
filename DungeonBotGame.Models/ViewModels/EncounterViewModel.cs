namespace DungeonBotGame.Models.ViewModels
{
    public record EncounterViewModel
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public EnemyType EnemyType { get; set; }

        public string ProfileImageLocation { get; init; }

        public EncounterViewModel(string name, string description, string profileImageLocation, EnemyType enemyType)
        {
            Name = name;
            Description = description;
            ProfileImageLocation = profileImageLocation;
            EnemyType = enemyType;
        }
    }
}
