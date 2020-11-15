using System.Collections.Generic;

namespace DungeonBotGame.Models.Combat
{
    public class Enemy : CharacterBase, IEnemy
    {
        public Enemy(string enemyName, int maximumHealth, IEnemyActionModule enemyActionModule, IDictionary<AbilityType, AbilityContext> abilities) :
            base(enemyName, maximumHealth, enemyActionModule.SourceCodeFiles, abilities)
        {
            EnemyActionModule = enemyActionModule;
        }

        public IEnemyActionModule EnemyActionModule { get; }
    }
}
