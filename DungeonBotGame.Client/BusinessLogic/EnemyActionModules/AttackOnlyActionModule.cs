using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.BusinessLogic.EnemyActionModules
{
    public class AttackOnlyActionModule : IEnemyActionModule
    {
        public IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent) => actionComponent.Attack(sensorComponent.DungeonBot);
    }
}
