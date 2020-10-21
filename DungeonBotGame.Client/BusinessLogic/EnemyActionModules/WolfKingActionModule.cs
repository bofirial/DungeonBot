using System.Linq;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.EnemyActionModules
{
    public class WolfKingActionModule : IEnemyActionModule
    {
        public IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent)
        {
            var previousRoundResult = sensorComponent.EncounterRoundHistory.LastOrDefault();

            if (previousRoundResult != null && previousRoundResult.ActionResults.Any(a => a.Action is IAbilityAction && a.Character == sensorComponent.DungeonBot))
            {
                //TODO: return actionComponent.UseLickWounds();
                return new AbilityAction(AbilityType.LickWounds);
            }

            return actionComponent.Attack(sensorComponent.DungeonBot);
        }
    }
}
