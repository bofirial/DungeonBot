using System.Linq;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.SourceGenerators.Attributes;

namespace DungeonBotGame.Client.BusinessLogic.EnemyActionModules
{
    [GenerateSourceCodePropertyPartialClass]
    public partial class MamaBearActionModule : IEnemyActionModule
    {
        [ActionModuleEntrypoint]
        public IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent)
        {
            if (actionComponent.SwipeIsAvailable())
            {
                return actionComponent.UseSwipe();
            }

            return actionComponent.Attack(sensorComponent.DungeonBots.First());
        }
    }
}
