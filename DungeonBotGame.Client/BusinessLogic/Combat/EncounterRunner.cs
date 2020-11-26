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
        private readonly IActionModuleExecuter _actionModuleExecuter;
        private readonly ICombatActionProcessor _combatActionProcessor;
        private readonly ICombatValueCalculator _combatValueCalculator;

        public EncounterRunner(IEnemyFactory enemyFactory, IActionModuleExecuter actionModuleExecuter, ICombatActionProcessor combatActionProcessor, ICombatValueCalculator combatValueCalculator)
        {
            _enemyFactory = enemyFactory;
            _actionModuleExecuter = actionModuleExecuter;
            _combatActionProcessor = combatActionProcessor;
            _combatValueCalculator = combatValueCalculator;
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
                    switch (combatEvent.CombatEventType)
                    {
                        case CombatEventType.CharacterAction:
                            await ProcessCharacterActionCombatEvent(combatEvent, combatContext);

                            break;

                        case CombatEventType.CooldownReset:

                            if (combatEvent is CombatEvent<AbilityType> abilityCooldownResetEvent)
                            {
                                combatEvent.Character.Abilities[abilityCooldownResetEvent.EventData] = combatEvent.Character.Abilities[abilityCooldownResetEvent.EventData] with { IsAvailable = true };
                            }

                            break;

                        case CombatEventType.CombatEffect:

                            if (combatEvent is CombatEvent<CombatEffect> combatEffectEvent)
                            {
                                switch (combatEffectEvent.EventData.CombatEffectType)
                                {
                                    case CombatEffectType.DamageOverTime:

                                        combatEffectEvent.Character.CurrentHealth -= combatEffectEvent.EventData.Value;

                                        if (combatEffectEvent.EventData.CombatTime <= combatContext.CombatTimer)
                                        {
                                            combatEffectEvent.Character.CombatEffects.Remove(combatEffectEvent.EventData);
                                        }
                                        else if (combatEffectEvent.EventData.CombatTimeInterval != null)
                                        {
                                            combatContext.NewCombatEvents.Add(combatEffectEvent with { CombatTime = combatContext.CombatTimer + combatEffectEvent.EventData.CombatTimeInterval.Value });
                                        }

                                        combatContext.CombatLog.Add(new CombatLogEntry(combatContext.CombatTimer, combatEffectEvent.Character, $"{combatEffectEvent.Character.Name} takes {combatEffectEvent.EventData.Value} damage from {combatEffectEvent.EventData.Name}.", null, combatContext.Characters.Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList()));

                                        break;
                                }
                            }

                            break;
                        default:
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
            combatContext.CombatLog = combatContext.Characters.Select(c => new CombatLogEntry(
                    combatContext.CombatTimer,
                    c,
                    $"{c.Name} enters combat.",
                    null,
                    combatContext.Characters.Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList()))
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

        private static void ResetCombatEffects(CharacterBase character, CombatContext combatContext)
        {
            character.CombatEffects.Clear();

            AddCombatEffectsForPassiveAbilities(character, combatContext);
        }

        private void AddInitialCombatEvents(CharacterBase character, CombatContext combatContext) =>
            combatContext.CombatEvents.Add(new CombatEvent(_combatValueCalculator.GetIterationsUntilNextAction(character), character, CombatEventType.CharacterAction));

        private static void AddCombatEffectsForPassiveAbilities(CharacterBase character, CombatContext combatContext)
        {
            foreach (var abilityType in character.Abilities.Keys)
            {
                switch (abilityType)
                {
                    case AbilityType.SurpriseAttack:
                        character.CombatEffects.Add(new CombatEffect("Element of Surprise - Attack Damage", CombatEffectType.AttackPercentage, Value: 200, CombatTime: null, CombatTimeInterval: null));
                        character.CombatEffects.Add(new CombatEffect("Element of Surprise - Immediate Action", CombatEffectType.ImmediateAction, Value: 1, CombatTime: null, CombatTimeInterval: null));
                        character.CombatEffects.Add(new CombatEffect("Element of Surprise - Stun Target", CombatEffectType.StunTarget, Value: 200, CombatTime: null, CombatTimeInterval: null));

                        combatContext.CombatLog.Add(new CombatLogEntry(0, character, $"{character.Name} has the element of surprise.", null, combatContext.Characters.Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList()));

                        break;

                    case AbilityType.SalvageStrikes:
                        character.CombatEffects.Add(new CombatEffect("Salvage Strikes", CombatEffectType.SalvageStrikes, Value: 1, CombatTime: null, CombatTimeInterval: null));

                        combatContext.CombatLog.Add(new CombatLogEntry(0, character, $"{character.Name} prepares salvage strikes.", null, combatContext.Characters.Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList()));

                        break;
                }
            }
        }

        private async Task ProcessCharacterActionCombatEvent(CombatEvent combatEvent, CombatContext combatContext)
        {
            if (combatEvent.Character.CurrentHealth <= 0)
            {
                return;
            }

            var startOfCharacterActionCombatEffectTypes = new CombatEffectType[] { CombatEffectType.Stunned };
            var startOfCharacterActionCombatEffects = combatEvent.Character.CombatEffects.Where(c => startOfCharacterActionCombatEffectTypes.Contains(c.CombatEffectType)).ToList();

            foreach (var startOfCharacterActionCombatEffect in startOfCharacterActionCombatEffects)
            {
                switch (startOfCharacterActionCombatEffect.CombatEffectType)
                {
                    case CombatEffectType.Stunned:

                        combatContext.CombatLog.Add(new CombatLogEntry(combatContext.CombatTimer, combatEvent.Character, $"{combatEvent.Character.Name} is stunned.", null, combatContext.Characters.Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList()));
                        combatContext.NewCombatEvents.Add(combatEvent with { CombatTime = combatContext.CombatTimer + startOfCharacterActionCombatEffect.Value });

                        combatEvent.Character.CombatEffects.Remove(startOfCharacterActionCombatEffect);

                        return;
                }
            }

            var actionComponent = new ActionComponent(combatEvent.Character);

            var sensorComponent = new SensorComponent(
                combatContext.DungeonBots.Where(d => d.CurrentHealth > 0).Cast<IDungeonBot>().ToImmutableList(),
                combatContext.Enemies.Where(e => e.CurrentHealth > 0).Cast<IEnemy>().ToImmutableList(),
                combatContext.CombatTimer,
                combatContext.CombatLog.Cast<ICombatLogEntry>().ToImmutableList());

            var action = combatEvent.Character switch
            {
                DungeonBot dungeonBot => await _actionModuleExecuter.ExecuteActionModule(dungeonBot, actionComponent, sensorComponent),
                Enemy enemy => await _actionModuleExecuter.ExecuteEnemyActionModule(enemy, actionComponent, sensorComponent),
                _ => throw new UnknownCharacterTypeException($"Unknown Character Type: {combatEvent.Character.GetType()}"),
            };

            _combatActionProcessor.ProcessAction(action, combatEvent.Character, combatContext);

            combatContext.NewCombatEvents.Add(combatEvent with { CombatTime = combatContext.CombatTimer + _combatValueCalculator.GetIterationsUntilNextAction(combatEvent.Character) });
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
