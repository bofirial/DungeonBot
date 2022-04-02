using System.Linq;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.SourceGenerators.Attributes;

namespace DungeonBotGame.Client.BusinessLogic.EnemyActionModules
{
    [GenerateSourceCodePropertyPartialClass]
    public partial class BabyBearActionModule : IEnemyActionModule
    {
        [ActionModuleEntrypoint]
        public IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent)
        {
            var mamaBear = sensorComponent.Enemies.FirstOrDefault(e => e.Name == "Mama Bear");

            if (mamaBear != null && (double)mamaBear.CurrentHealth / mamaBear.MaximumHealth < 0.75 && actionComponent.RepairIsAvailable())
            {
                return actionComponent.UseRepair(mamaBear);
            }

            return actionComponent.Attack(sensorComponent.DungeonBots.First());
        }
    }
}
