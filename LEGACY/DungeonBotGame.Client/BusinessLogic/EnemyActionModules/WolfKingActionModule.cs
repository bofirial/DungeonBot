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
            var previousDungeonBotActionCombatLogEntry = sensorComponent.CombatLog.LastOrDefault(a => a is CombatLogEntry<IAction> && a.Character is IDungeonBot);

            if (actionComponent.LickWoundsIsAvailable() &&
                previousDungeonBotActionCombatLogEntry != null &&
                previousDungeonBotActionCombatLogEntry is CombatLogEntry<IAction> previousDungeonBotAction &&
                previousDungeonBotAction.LogData is IAbilityAction)
            {
                return actionComponent.UseLickWounds();
            }

            return actionComponent.Attack(sensorComponent.DungeonBots.First());
        }
    }
}
