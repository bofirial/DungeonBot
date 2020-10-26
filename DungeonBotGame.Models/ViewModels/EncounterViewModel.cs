namespace DungeonBotGame.Models.ViewModels
{
    public record EncounterViewModel
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public string ProfileImageLocation { get; init; }

        public EncounterViewModel(string name, string description, string profileImageLocation)
        {
            Name = name;
            Description = description;
            ProfileImageLocation = profileImageLocation;
        }
    }
}
