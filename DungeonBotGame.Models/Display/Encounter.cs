namespace DungeonBotGame.Models.Display
{
    public class Encounter
    {
        public string Name { get; }

        public string Description { get; }

        public string ProfileImageLocation { get; }

        public Encounter(string name, string description, string profileImageLocation)
        {
            Name = name;
            Description = description;
            ProfileImageLocation = profileImageLocation;
        }
    }
}
