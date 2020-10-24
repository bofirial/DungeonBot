using DungeonBotGame.Models.Combat;
using DungeonBotGame.SourceGenerators.Attributes;

namespace DungeonBotGame.Client.BusinessLogic.EnemyActionModules
{
    [GenerateSourceCodePropertyPartialClass]
    public partial class AttackOnlyActionModule : IEnemyActionModule
    {
        public IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent) => actionComponent.Attack(sensorComponent.DungeonBot);
    }
}
