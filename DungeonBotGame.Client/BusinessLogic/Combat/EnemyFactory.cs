using System;
using System.Collections.Immutable;
using DungeonBotGame.Client.BusinessLogic.EnemyActionModules;
using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface IEnemyFactory
    {
        IImmutableList<Enemy> CreateEnemies(EncounterViewModel encounter);
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

        public IImmutableList<Enemy> CreateEnemies(EncounterViewModel encounter)
        {
            var enemies = encounter.EnemyType switch
            {
                EnemyType.Rat => ImmutableList.Create(new Enemy(encounter.Name, level: 1, power: 3, armor: 3, speed: 5, new AttackOnlyActionModule(), _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(Array.Empty<AbilityType>()))),
                EnemyType.Dragon => ImmutableList.Create(new Enemy(encounter.Name, level: 1, power: 8, armor: 5, speed: 5, new AttackOnlyActionModule(), _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(Array.Empty<AbilityType>()))),
                EnemyType.Wolf => ImmutableList.Create(new Enemy(encounter.Name, level: 1, power: 8, armor: 3, speed: 5, new WolfKingActionModule(), _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(new AbilityType[] { AbilityType.LickWounds }))),
                _ => throw new UnknownEnemyTypeException(encounter.EnemyType),
            };

            foreach (var enemy in enemies)
            {
                enemy.MaximumHealth = _combatValueCalculator.GetMaximumHealth(enemy);
                enemy.CurrentHealth = enemy.MaximumHealth;
            }

            return enemies;
        }
    }
}
