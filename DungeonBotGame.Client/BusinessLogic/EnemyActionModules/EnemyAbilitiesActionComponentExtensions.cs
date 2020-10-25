using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.EnemyActionModules
{
    public static class EnemyAbilitiesActionComponentExtensions
    {
        public static IAction UseLickWounds(this IActionComponent actionComponent) 
            => new AbilityAction(AbilityType.LickWounds);
    }
}
