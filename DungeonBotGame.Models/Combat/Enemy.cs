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
            IDictionary<AbilityType, AbilityContext> abilities,
            IList<CombatEffect> combatEffects) :
            base(enemyName, level, power, armor, speed, enemyActionModule.SourceCodeFiles, abilities, combatEffects)
        {
            EnemyActionModule = enemyActionModule;
        }

        public IEnemyActionModule EnemyActionModule { get; }
    }
}
