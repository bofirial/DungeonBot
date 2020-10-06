namespace DungeonBot.Models.Combat
{
    public class Enemy : CharacterBase, IEnemy
    {
        public Enemy(string enemyName, int maximumHealth, IEnemyActionModule enemyActionModule) : base(enemyName, maximumHealth)
        {
            EnemyActionModule = enemyActionModule;
        }

        public IEnemyActionModule EnemyActionModule { get; }
    }
}
