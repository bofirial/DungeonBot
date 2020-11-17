using System.Collections.Generic;

namespace DungeonBotGame.Models.Combat
{
    public class Enemy : CharacterBase, IEnemy
    {
        public Enemy(string enemyName,
            short level,
            short power,
            short armor,
            short speed,
            IEnemyActionModule enemyActionModule,
            IDictionary<AbilityType, AbilityContext> abilities) :
            base(enemyName, level, power, armor, speed, enemyActionModule.SourceCodeFiles, abilities)
        {
            EnemyActionModule = enemyActionModule;
        }

        public IEnemyActionModule EnemyActionModule { get; }
    }
}
