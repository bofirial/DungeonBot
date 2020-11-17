using System;
using DungeonBotGame.Client.BusinessLogic.EnemyActionModules;
using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface IEnemyFactory
    {
        Enemy CreateEnemy(EncounterViewModel encounter);
    }

    public class EnemyFactory : IEnemyFactory
    {
        private readonly IAbilityContextDictionaryBuilder _abilityContextDictionaryBuilder;
        private readonly ICombatValueCalculator _combatValueCalculator;

        public EnemyFactory(IAbilityContextDictionaryBuilder abilityContextDictionaryBuilder, ICombatValueCalculator combatValueCalculator)
        {
            _abilityContextDictionaryBuilder = abilityContextDictionaryBuilder;
            _combatValueCalculator = combatValueCalculator;
        }

        public Enemy CreateEnemy(EncounterViewModel encounter)
        {
            var enemy = encounter.EnemyType switch
            {
                EnemyType.Rat => new Enemy(encounter.Name, level: 1, power: 3, armor: 3, speed: 12, new AttackOnlyActionModule(), _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(Array.Empty<AbilityType>())),
                EnemyType.Dragon => new Enemy(encounter.Name, level: 1, power: 8, armor: 5, speed: 5, new AttackOnlyActionModule(), _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(Array.Empty<AbilityType>())),
                EnemyType.Wolf => new Enemy(encounter.Name, level: 1, power: 8, armor: 3, speed: 5, new WolfKingActionModule(), _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(new AbilityType[] { AbilityType.LickWounds })),
                _ => throw new UnknownEnemyTypeException(encounter.EnemyType),
            };

            enemy.MaximumHealth = _combatValueCalculator.GetMaximumHealth(enemy);
            enemy.CurrentHealth = enemy.MaximumHealth;

            return enemy;
        }
    }
}
