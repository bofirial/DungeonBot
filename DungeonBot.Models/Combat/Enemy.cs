using System.Collections.Generic;

namespace DungeonBot.Models.Combat
{
    public class Enemy : CharacterBase, IEnemy
    {
        public Enemy(string enemyName, int maximumHealth, IEnemyActionModule enemyActionModule, Dictionary<AbilityType, AbilityContext> abilities) :
            base(enemyName, maximumHealth, abilities)
        {
            EnemyActionModule = enemyActionModule;
        }

        public IEnemyActionModule EnemyActionModule { get; }
    }
}
