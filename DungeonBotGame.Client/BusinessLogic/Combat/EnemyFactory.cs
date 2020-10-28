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

        public EnemyFactory(IAbilityContextDictionaryBuilder abilityContextDictionaryBuilder)
        {
            _abilityContextDictionaryBuilder = abilityContextDictionaryBuilder;
        }

        public Enemy CreateEnemy(EncounterViewModel encounter)
        {
            return encounter.EnemyType switch
            {
                EnemyType.Rat => new Enemy(encounter.Name, 80, new AttackOnlyActionModule(), _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(Array.Empty<AbilityType>())),
                EnemyType.Dragon => new Enemy(encounter.Name, 100, new AttackOnlyActionModule(), _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(Array.Empty<AbilityType>())),
                EnemyType.Wolf => new Enemy(encounter.Name, 80, new WolfKingActionModule(), _abilityContextDictionaryBuilder.BuildAbilityContextDictionary(new AbilityType[] { AbilityType.LickWounds })),
                _ => throw new UnknownEnemyTypeException(encounter.EnemyType),
            };
        }
    }
}
