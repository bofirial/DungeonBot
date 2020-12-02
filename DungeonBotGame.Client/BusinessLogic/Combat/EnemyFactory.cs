using System;
using System.Collections.Generic;
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
                EnemyType.Rat => ImmutableList.Create(
                    new Enemy(
                        encounter.Name,
                        level: 1,
                        power: 3,
                        armor: 3,
                        speed: 5,
                        new AttackOnlyActionModule(),
                        _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(Array.Empty<AbilityType>()),
                        new List<CombatEffect>()
                    )
                ),

                EnemyType.Dragon => ImmutableList.Create(
                    new Enemy(
                        encounter.Name,
                        level: 1,
                        power: 8,
                        armor: 5,
                        speed: 5,
                        new AttackOnlyActionModule(),
                        _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(Array.Empty<AbilityType>()),
                        new List<CombatEffect>()
                    )
                ),

                EnemyType.Wolf => ImmutableList.Create(
                    new Enemy(
                        encounter.Name,
                        level: 1,
                        power: 8,
                        armor: 3,
                        speed: 5,
                        new WolfKingActionModule(),
                        _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(new[] { AbilityType.LickWounds }),
                        new List<CombatEffect>()
                    )
                ),

                EnemyType.Pixie => ImmutableList.Create(
                    new Enemy(
                        encounter.Name,
                        level: 1,
                        power: 1,
                        armor: 4,
                        speed: 25,
                        new AttackOnlyActionModule(),
                        _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(Array.Empty<AbilityType>()),
                        new List<CombatEffect>()
                    )
                ),

                EnemyType.Troll => ImmutableList.Create(
                    new Enemy(
                        encounter.Name,
                        level: 1,
                        power: 10,
                        armor: 6,
                        speed: 1,
                        new AttackOnlyActionModule(),
                        _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(Array.Empty<AbilityType>()),
                        new List<CombatEffect>()
                    )
                ),

                EnemyType.Bat => ImmutableList.Create(
                    new Enemy(
                        "Bat 1",
                        level: 1,
                        power: 0,
                        armor: 1,
                        speed: 14,
                        new AttackOnlyActionModule(),
                        _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(Array.Empty<AbilityType>()),
                        new List<CombatEffect>()
                    ),
                    new Enemy(
                        "Bat 2",
                        level: 1,
                        power: 0,
                        armor: 1,
                        speed: 14,
                        new AttackOnlyActionModule(),
                        _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(Array.Empty<AbilityType>()),
                        new List<CombatEffect>()
                    ),
                    new Enemy(
                        "Bat 3",
                        level: 1,
                        power: 0,
                        armor: 1,
                        speed: 14,
                        new AttackOnlyActionModule(),
                        _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(Array.Empty<AbilityType>()),
                        new List<CombatEffect>()
                    ),
                    new Enemy(
                        "Bat 4",
                        level: 1,
                        power: 0,
                        armor: 1,
                        speed: 14,
                        new AttackOnlyActionModule(),
                        _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(Array.Empty<AbilityType>()),
                        new List<CombatEffect>()
                    ),
                    new Enemy(
                        "Bat 5",
                        level: 1,
                        power: 0,
                        armor: 1,
                        speed: 14,
                        new AttackOnlyActionModule(),
                        _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(Array.Empty<AbilityType>()),
                        new List<CombatEffect>()
                    )
                ),

                EnemyType.EliteBearFamily => ImmutableList.Create(
                    new Enemy(
                        "Baby Bear",
                        level: 1,
                        power: 1,
                        armor: 4,
                        speed: 1,
                        new BabyBearActionModule(),
                        _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(new[] { AbilityType.Repair }),
                        new List<CombatEffect>()
                    ),
                    new Enemy(
                        "Mama Bear",
                        level: 1,
                        power: 8,
                        armor: 9,
                        speed: 8,
                        new MamaBearActionModule(),
                        _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(new[] { AbilityType.Swipe, AbilityType.ProtectBabies }),
                        new List<CombatEffect>()
                    ),
                    new Enemy(
                        "Baby Bear",
                        level: 1,
                        power: 1,
                        armor: 4,
                        speed: 1,
                        new BabyBearActionModule(),
                        _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(new[] { AbilityType.Repair }),
                        new List<CombatEffect>()
                    )
                ),

                _ => throw new UnknownEnemyTypeException(encounter.EnemyType),
            };

            foreach (var enemy in enemies)
            {
                enemy.MaximumHealth = _combatValueCalculator.GetMaximumHealth(enemy);

                if (enemy.Name.Contains("Bat"))
                {
                    enemy.MaximumHealth = 40;
                }

                enemy.CurrentHealth = enemy.MaximumHealth;
            }

            return enemies;
        }
    }
}
