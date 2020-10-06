namespace DungeonBot.Models.Combat
{
    public class AttackAction : ITargettedAction
    {
        private readonly IEnemy _targetEnemy;

        public AttackAction(IEnemy targetEnemy)
        {
            _targetEnemy = targetEnemy;
        }

        public ITarget Target => _targetEnemy;

        public ActionType ActionType => ActionType.Attack;
    }
}
