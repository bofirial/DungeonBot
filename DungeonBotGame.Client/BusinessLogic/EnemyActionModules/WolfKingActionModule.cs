using System.Linq;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.SourceGenerators.Attributes;

namespace DungeonBotGame.Client.BusinessLogic.EnemyActionModules
{
    [GenerateSourceCodePropertyPartialClass]
    public partial class WolfKingActionModule : IEnemyActionModule
    {
        [ActionModuleEntrypoint]
        public IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent)
        {
            var previousActionResult = sensorComponent.ActionResults.LastOrDefault(a => a.Character is IDungeonBot);

            if (previousActionResult != null && previousActionResult.Action is IAbilityAction)
            {
                return actionComponent.UseLickWounds();
            }

            return actionComponent.Attack(sensorComponent.DungeonBots.First());
        }
    }
}
