namespace DungeonBotGame.Models.Combat
{
    public class ActionComponent : IActionComponent
    {
        private readonly CharacterBase _character;

        public ActionComponent(CharacterBase character)
        {
            _character = character;
        }

        public bool AbilityIsAvailable(AbilityType abilityType) =>
            _character.Abilities.ContainsKey(abilityType) && _character.Abilities[abilityType].CurrentCooldownRounds == 0;

        public ITargettedAction Attack(ITarget attackTarget) => new AttackAction(attackTarget);

        public ITargettedAbilityAction UseAbility(ITarget target, AbilityType abilityType) => new TargettedAbilityAction(target, abilityType);
    }
}
