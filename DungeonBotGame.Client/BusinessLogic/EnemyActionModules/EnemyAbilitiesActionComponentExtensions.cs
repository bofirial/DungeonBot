using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.EnemyActionModules
{
    public static class EnemyAbilitiesActionComponentExtensions
    {
        public static bool LickWoundsIsAvailable(this IActionComponent actionComponent)
            => ((ActionComponent)actionComponent).AbilityIsAvailable(AbilityType.LickWounds);

        public static IAction UseLickWounds(this IActionComponent actionComponent)
            => new AbilityAction(AbilityType.LickWounds);

        public static bool SwipeIsAvailable(this IActionComponent actionComponent)
            => ((ActionComponent)actionComponent).AbilityIsAvailable(AbilityType.Swipe);

        public static IAction UseSwipe(this IActionComponent actionComponent)
            => new AbilityAction(AbilityType.Swipe);

        public static bool RepairIsAvailable(this IActionComponent actionComponent)
            => ((ActionComponent)actionComponent).AbilityIsAvailable(AbilityType.Repair);

        public static IAction UseRepair(this IActionComponent actionComponent, ITarget target)
            => new TargettedAbilityAction(target, AbilityType.Repair);
    }
}
