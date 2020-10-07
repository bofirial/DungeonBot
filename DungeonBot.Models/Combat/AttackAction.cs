﻿namespace DungeonBot.Models.Combat
{
    public class AttackAction : ITargettedAction
    {
        private readonly ITarget _attackTarget;

        public AttackAction(ITarget attackTarget)
        {
            _attackTarget = attackTarget;
        }

        public ITarget Target => _attackTarget;

        public ActionType ActionType => ActionType.Attack;
    }
}
