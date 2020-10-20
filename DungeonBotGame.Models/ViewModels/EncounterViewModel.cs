namespace DungeonBotGame.Models.ViewModels
{
    public class EncounterViewModel
    {
        public string Name { get; }

        public string Description { get; }

        public string ProfileImageLocation { get; }

        public EncounterViewModel(string name, string description, string profileImageLocation)
        {
            Name = name;
            Description = description;
            ProfileImageLocation = profileImageLocation;
        }
    }
}
