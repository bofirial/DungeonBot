namespace DungeonBotGame.Models.Combat
{
    public class AbilityAction : IAbilityAction
    {
        public AbilityAction(AbilityType abilityType)
        {
            AbilityType = abilityType;
        }

        public AbilityType AbilityType { get; }

        public ActionType ActionType => ActionType.Ability;
    }
}
