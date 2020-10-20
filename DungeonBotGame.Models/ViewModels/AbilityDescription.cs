namespace DungeonBotGame.Models.ViewModels
{
    public class AbilityDescriptionViewModel
    {
        public string Name { get; }

        public string Description { get; }

        public AbilityType AbilityType { get; }

        public int CooldownRounds { get; }

        public AbilityDescriptionViewModel(string name, string description, AbilityType abilityType, int cooldownRounds)
        {
            Name = name;
            Description = description;
            AbilityType = abilityType;
            CooldownRounds = cooldownRounds;
        }
    }
}
