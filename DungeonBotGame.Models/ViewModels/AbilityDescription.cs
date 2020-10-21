namespace DungeonBotGame.Models.ViewModels
{
    public class AbilityDescriptionViewModel
    {
        public string Name { get; }

        public string Description { get; }

        public AbilityType AbilityType { get; }

        public int CooldownRounds { get; }

        public bool IsTargettedAbility { get; set; }

        public AbilityDescriptionViewModel(string name, string description, AbilityType abilityType, int cooldownRounds, bool isTargettedAbility)
        {
            Name = name;
            Description = description;
            AbilityType = abilityType;
            CooldownRounds = cooldownRounds;
            IsTargettedAbility = isTargettedAbility;
        }
    }
}
