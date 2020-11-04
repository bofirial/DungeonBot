namespace DungeonBotGame.Models.ViewModels
{
    public record EncounterViewModel
    {
        public string Name { get; init; }

        public int Order { get; init; }

        public string Description { get; init; }

        public EnemyType EnemyType { get; init; }

        public string ProfileImageLocation { get; init; }

        public EncounterViewModel(string name, int order, string description, string profileImageLocation, EnemyType enemyType)
        {
            Name = name;
            Order = order;
            Description = description;
            ProfileImageLocation = profileImageLocation;
            EnemyType = enemyType;
        }
    }
}
