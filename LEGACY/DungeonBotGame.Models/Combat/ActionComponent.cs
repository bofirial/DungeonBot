using System.Diagnostics.CodeAnalysis;

namespace DungeonBotGame.Models.Combat
{
    public class ActionComponent : IActionComponent
    {
        private readonly CharacterBase _character;

        public ActionComponent(CharacterBase character)
        {
            _character = character;
        }

        public ITargettedAction Attack(ITarget attackTarget) => new AttackAction(attackTarget);

        public bool AbilityIsAvailable(AbilityType abilityType) =>
            _character.Abilities.ContainsKey(abilityType) && _character.Abilities[abilityType].IsAvailable;


        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "May access instance data in the future.")]
        public IAbilityAction UseAbility(AbilityType abilityType) => new AbilityAction(abilityType);

        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "May access instance data in the future.")]
        public ITargettedAbilityAction UseTargettedAbility(ITarget target, AbilityType abilityType) => new TargettedAbilityAction(target, abilityType);
    }
}
