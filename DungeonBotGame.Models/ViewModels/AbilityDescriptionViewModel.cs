namespace DungeonBotGame.Models.ViewModels
{
    public record AbilityDescriptionViewModel
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public AbilityType AbilityType { get; init; }

        public int CooldownRounds { get; init; }

        public bool IsTargettedAbility { get; init; }

        public int StartOfCombatCooldownRounds { get; init; }

        public AbilityDescriptionViewModel(string name, string description, AbilityType abilityType, int cooldownRounds, bool isTargettedAbility, int startOfCombatCooldownRounds)
        {
            Name = name;
            Description = description;
            AbilityType = abilityType;
            CooldownRounds = cooldownRounds;
            IsTargettedAbility = isTargettedAbility;
            StartOfCombatCooldownRounds = startOfCombatCooldownRounds;
        }
    }
}
