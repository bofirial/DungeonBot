using DungeonBot.Models.Combat;

namespace DungeonBot.Client.BusinessLogic.EnemyActionModules
{
    public class AttackOnlyActionModule : IEnemyActionModule
    {
        public IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent) => actionComponent.Attack(sensorComponent.DungeonBot);
    }
}
