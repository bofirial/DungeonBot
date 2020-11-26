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
            var previousDungeonBotAction = sensorComponent.CombatLog.LastOrDefault(a => a.Character is IDungeonBot);

            if (previousDungeonBotAction != null && previousDungeonBotAction.Action is IAbilityAction)
            {
                return actionComponent.UseLickWounds();
            }

            return actionComponent.Attack(sensorComponent.DungeonBots.First());
        }
    }
}
