using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using DungeonBotGame.Client.ErrorHandling;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic.Combat
{
    public interface IEncounterRunner
    {
        Task<EncounterResultViewModel> RunAdventureEncounterAsync(IImmutableList<DungeonBot> dungeonBots, EncounterViewModel encounter);
    }

    public class EncounterRunner : IEncounterRunner
    {
        private const int MAX_COMBAT_TIME = 6000;

        private readonly IEnemyFactory _enemyFactory;
        private readonly ICombatValueCalculator _combatValueCalculator;
        private readonly ICombatLogEntryBuilder _combatLogEntryBuilder;
        private readonly IDictionary<CombatEventType, ICombatEventProcessor> _combatEventProcessors;

        public EncounterRunner(IEnemyFactory enemyFactory, ICombatValueCalculator combatValueCalculator, ICombatLogEntryBuilder combatLogEntryBuilder, IEnumerable<ICombatEventProcessor> combatEventProcessors)
        {
            _enemyFactory = enemyFactory;
            _combatValueCalculator = combatValueCalculator;
            _combatLogEntryBuilder = combatLogEntryBuilder;

            _combatEventProcessors = combatEventProcessors.ToDictionary(p => p.CombatEventType, p => p);
        }

        private static bool EncounterHasCompleted(CombatContext combatContext) =>
            AllDungeonBotsHaveFallen(combatContext.Characters) ||
            AllEnemiesHaveFallen(combatContext.Characters) ||
            combatContext.CombatTimer >= MAX_COMBAT_TIME;

        private static bool AllDungeonBotsHaveFallen(IEnumerable<CharacterBase> characters) => characters.All(c => c.CurrentHealth <= 0 || c is Enemy);

        private static bool AllEnemiesHaveFallen(IEnumerable<CharacterBase> characters) => characters.All(c => c.CurrentHealth <= 0 || c is DungeonBot);

        public async Task<EncounterResultViewModel> RunAdventureEncounterAsync(IImmutableList<DungeonBot> dungeonBots, EncounterViewModel encounter)
        {
            var combatContext = BuildCombatContext(dungeonBots, encounter);

            InitializeEncounter(combatContext);

            while (!EncounterHasCompleted(combatContext))
            {
                await ProcessNextCombatEvents(combatContext);
            }

            return BuildEncounterResult(encounter, combatContext);
        }

        private async Task ProcessNextCombatEvents(CombatContext combatContext)
        {
            var processedCombatEvents = new List<CombatEvent>();

            foreach (var combatEvent in combatContext.CombatEvents)
            {
                if (combatContext.CombatTimer >= combatEvent.CombatTime)
                {
                    if (_combatEventProcessors.ContainsKey(combatEvent.CombatEventType))
                    {
                        await _combatEventProcessors[combatEvent.CombatEventType].ProcessCombatEvent(combatEvent, combatContext);
                    }
                    else
                    {
                        throw new UnknownCombatEventTypeException(combatEvent.CombatEventType);
                    }

                    processedCombatEvents.Add(combatEvent);

                    if (EncounterHasCompleted(combatContext))
                    {
                        break;
                    }
                }
            }

            foreach (var processedCombatEvent in processedCombatEvents)
            {
                combatContext.CombatEvents.Remove(processedCombatEvent);
            }

            combatContext.CombatEvents.AddRange(combatContext.NewCombatEvents);
            combatContext.NewCombatEvents.Clear();

            combatContext.CombatTimer = combatContext.CombatEvents.Min(e => e.CombatTime);
        }

        private void InitializeEncounter(CombatContext combatContext)
        {
            foreach (var character in combatContext.Characters)
            {
                ResetAbilityAvailability(character);
                ResetCombatEffects(character, combatContext);
                AddInitialCombatEvents(character, combatContext);
            }
        }

        private CombatContext BuildCombatContext(IImmutableList<DungeonBot> dungeonBots, EncounterViewModel encounter)
        {
            var combatContext = new CombatContext()
            {
                DungeonBots = dungeonBots,
                Enemies = _enemyFactory.CreateEnemies(encounter)
            };

            combatContext.Characters = CreateCharacterList(combatContext.DungeonBots, combatContext.Enemies);
            combatContext.CombatLog = combatContext.Characters.Select(c => _combatLogEntryBuilder.CreateCombatLogEntry(
                    $"{c.Name} enters combat.",
                    c,
                    combatContext))
                .ToList();

            combatContext.CombatEvents = new List<CombatEvent>();
            combatContext.NewCombatEvents = new List<CombatEvent>();
            return combatContext;
        }

        private static List<CharacterBase> CreateCharacterList(IImmutableList<DungeonBot> dungeonBots, IImmutableList<Enemy> enemies)
        {
            var characterList = new List<CharacterBase>();

            characterList.AddRange(dungeonBots);
            characterList.AddRange(enemies);

            return characterList.ToList();
        }

        private static void ResetAbilityAvailability(CharacterBase character)
        {
            foreach (var abilityType in character.Abilities.Keys)
            {
                character.Abilities[abilityType] = character.Abilities[abilityType] with { IsAvailable = true };
            }
        }

        private void ResetCombatEffects(CharacterBase character, CombatContext combatContext)
        {
            character.CombatEffects.Clear();

            AddCombatEffectsForPassiveAbilities(character, combatContext);
        }

        private void AddInitialCombatEvents(CharacterBase character, CombatContext combatContext) =>
            combatContext.CombatEvents.Add(new CombatEvent(_combatValueCalculator.GetIterationsUntilNextAction(character), character, CombatEventType.CharacterAction));

        private void AddCombatEffectsForPassiveAbilities(CharacterBase character, CombatContext combatContext)
        {
            foreach (var abilityType in character.Abilities.Keys)
            {
                switch (abilityType)
                {
                    case AbilityType.SurpriseAttack:
                        character.CombatEffects.Add(new CombatEffect("Element of Surprise - Double Attack Damage", "2x Damage", CombatEffectType.AttackPercentage, Value: 200));
                        character.CombatEffects.Add(new CombatEffect("Element of Surprise - Immediate Action", "Instant Action", CombatEffectType.ImmediateAction, Value: 1));
                        character.CombatEffects.Add(new CombatEffect("Element of Surprise - Stun Target", "Stun Target", CombatEffectType.StunTarget, Value: 200));

                        combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry($"{character.Name} has the element of surprise.", character, combatContext));

                        break;

                    case AbilityType.SalvageStrikes:
                        character.CombatEffects.Add(new CombatEffect("Salvage Strikes", "Salvage Strikes", CombatEffectType.SalvageStrikes, Value: 1));

                        combatContext.CombatLog.Add(_combatLogEntryBuilder.CreateCombatLogEntry($"{character.Name} prepares salvage strikes.", character, combatContext));

                        break;
                }
            }
        }

        private static EncounterResultViewModel BuildEncounterResult(EncounterViewModel encounter, CombatContext combatContext)
        {
            var resultDisplayText = string.Empty;
            var success = false;

            if (AllDungeonBotsHaveFallen(combatContext.Characters))
            {
                resultDisplayText = $"{(combatContext.Enemies.Count > 1 ? "The enemies have" : $"{combatContext.Enemies[0].Name} has")} defeated {(combatContext.DungeonBots.Count > 1 ? "the DungeonBots" : combatContext.DungeonBots[0].Name)}.";
                success = false;
            }
            else if (AllEnemiesHaveFallen(combatContext.Characters))
            {
                resultDisplayText = $"{(combatContext.DungeonBots.Count > 1 ? "The DungeonBots have" : $"{combatContext.DungeonBots[0].Name} has")} defeated {(combatContext.Enemies.Count > 1 ? "the enemies" : combatContext.Enemies[0].Name)}.";
                success = true;
            }
            else if (combatContext.CombatTimer >= MAX_COMBAT_TIME)
            {
                resultDisplayText = $"{(combatContext.DungeonBots.Count > 1 ? "The DungeonBots have" : $"{combatContext.DungeonBots[0].Name} has")} run out of time to defeat {(combatContext.Enemies.Count > 1 ? "the enemies" : combatContext.Enemies[0].Name)}.";
                success = false;
            }

            return new EncounterResultViewModel(encounter.Name, encounter.Order, Success: success, combatContext.CombatLog.ToImmutableList(), resultDisplayText, CreateCharacterList(combatContext.DungeonBots, combatContext.Enemies).ToImmutableList());
        }
    }
}
