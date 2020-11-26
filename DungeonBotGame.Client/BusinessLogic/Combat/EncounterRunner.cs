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

        bool EncounterHasCompleted(IEnumerable<CharacterBase> characters, int combatTimer);
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

        public bool EncounterHasCompleted(IEnumerable<CharacterBase> characters, int combatTimer) =>
            AllDungeonBotsHaveFallen(characters) ||
            AllEnemiesHaveFallen(characters) ||
            combatTimer >= MAX_COMBAT_TIME;

        private static bool AllDungeonBotsHaveFallen(IEnumerable<CharacterBase> characters) => characters.All(c => c.CurrentHealth <= 0 || c is Enemy);
        private static bool AllEnemiesHaveFallen(IEnumerable<CharacterBase> characters) => characters.All(c => c.CurrentHealth <= 0 || c is DungeonBot);

        public async Task<EncounterResultViewModel> RunAdventureEncounterAsync(IImmutableList<DungeonBot> dungeonBots, EncounterViewModel encounter)
        {
            var combatTimer = 0;

            var enemies = _enemyFactory.CreateEnemies(encounter);
            var characters = CreateCharacterList(dungeonBots, enemies);

            var actionResults = characters.Select(c => new ActionResult(
                    combatTimer,
                    c,
                    $"{c.Name} enters combat.",
                    null,
                    characters.Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList(),
                    ImmutableList.Create<CombatEvent>()))
                .ToList();

            foreach (var character in characters)
            {
                ResetAbilityAvailability(character);
                ResetCombatEffects(character, actionResults, characters);
            }

            var combatEvents = characters.Select(c => new CombatEvent(_combatValueCalculator.GetIterationsUntilNextAction(c), c, CombatEventType.CharacterAction)).ToList();

            while (!EncounterHasCompleted(characters, combatTimer))
            {
                var processedCombatEvents = new List<CombatEvent>();
                var newCombatEvents = new List<CombatEvent>();

                foreach (var combatEvent in combatEvents)
                {
                    if (combatTimer >= combatEvent.CombatTime)
                    {
                        switch (combatEvent.CombatEventType)
                        {
                            case CombatEventType.CharacterAction:
                                await ProcessCharacterActionCombatEvent(dungeonBots, combatTimer, enemies, actionResults, newCombatEvents, combatEvent);

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

                                            if (combatEffectEvent.EventData.CombatTime <= combatTimer)
                                            {
                                                combatEffectEvent.Character.CombatEffects.Remove(combatEffectEvent.EventData);
                                            }
                                            else if (combatEffectEvent.EventData.CombatTimeInterval != null)
                                            {
                                                newCombatEvents.Add(combatEffectEvent with { CombatTime = combatTimer + combatEffectEvent.EventData.CombatTimeInterval.Value });
                                            }

                                            actionResults.Add(new ActionResult(combatTimer, combatEffectEvent.Character, $"{combatEffectEvent.Character.Name} takes {combatEffectEvent.EventData.Value} damage from {combatEffectEvent.EventData.Name}.", null, characters.Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList(), ImmutableList.Create<CombatEvent>()));

                                            break;
                                    }
                                }

                                break;
                            default:
                                throw new UnknownCombatEventTypeException(combatEvent.CombatEventType);
                        }

                        processedCombatEvents.Add(combatEvent);

                        if (EncounterHasCompleted(characters, combatTimer))
                        {
                            break;
                        }
                    }
                }

                foreach (var processedCombatEvent in processedCombatEvents)
                {
                    combatEvents.Remove(processedCombatEvent);
                }

                combatEvents.AddRange(newCombatEvents);

                combatTimer = combatEvents.Min(e => e.CombatTime);
            }

            var resultDisplayText = string.Empty;
            var success = false;

            if (AllDungeonBotsHaveFallen(characters))
            {
                resultDisplayText = $"{(enemies.Count > 1 ? "The enemies have" : $"{enemies[0].Name} has")} defeated {(dungeonBots.Count > 1 ? "the DungeonBots" : dungeonBots[0].Name)}.";
                success = false;
            }
            else if (AllEnemiesHaveFallen(characters))
            {
                resultDisplayText = $"{(dungeonBots.Count > 1 ? "The DungeonBots have" : $"{dungeonBots[0].Name} has")} defeated {(enemies.Count > 1 ? "the enemies" : enemies[0].Name)}.";
                success = true;
            }
            else if (combatTimer >= MAX_COMBAT_TIME)
            {
                resultDisplayText = $"{(dungeonBots.Count > 1 ? "The DungeonBots have" : $"{dungeonBots[0].Name} has")} run out of time to defeat {(enemies.Count > 1 ? "the enemies" : enemies[0].Name)}.";
                success = false;
            }

            return new EncounterResultViewModel(encounter.Name, encounter.Order, Success: success, actionResults.ToImmutableList(), resultDisplayText, characters);
        }

        private static void ResetAbilityAvailability(CharacterBase character)
        {
            foreach (var abilityType in character.Abilities.Keys)
            {
                character.Abilities[abilityType] = character.Abilities[abilityType] with { IsAvailable = true };
            }
        }

        private static void ResetCombatEffects(CharacterBase character, List<ActionResult> actionResults, IImmutableList<CharacterBase> characters)
        {
            character.CombatEffects.Clear();

            AddCombatEffectsForPassiveAbilities(character, actionResults, characters);
        }

        private static void AddCombatEffectsForPassiveAbilities(CharacterBase character, List<ActionResult> actionResults, IImmutableList<CharacterBase> characters)
        {
            foreach (var abilityType in character.Abilities.Keys)
            {
                switch (abilityType)
                {
                    case AbilityType.SurpriseAttack:
                        character.CombatEffects.Add(new CombatEffect("Element of Surprise - Attack Damage", CombatEffectType.AttackPercentage, Value: 200, CombatTime: null, CombatTimeInterval: null));
                        character.CombatEffects.Add(new CombatEffect("Element of Surprise - Immediate Action", CombatEffectType.ImmediateAction, Value: 1, CombatTime: null, CombatTimeInterval: null));
                        character.CombatEffects.Add(new CombatEffect("Element of Surprise - Stun Target", CombatEffectType.StunTarget, Value: 200, CombatTime: null, CombatTimeInterval: null));

                        actionResults.Add(new ActionResult(0, character, $"{character.Name} has the element of surprise.", null, characters.Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList(), ImmutableList.Create<CombatEvent>()));

                        break;

                    case AbilityType.SalvageStrikes:
                        character.CombatEffects.Add(new CombatEffect("Salvage Strikes", CombatEffectType.SalvageStrikes, Value: 1, CombatTime: null, CombatTimeInterval: null));

                        actionResults.Add(new ActionResult(0, character, $"{character.Name} prepares their salvage strikes.", null, characters.Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList(), ImmutableList.Create<CombatEvent>()));

                        break;
                }
            }
        }

        private async Task ProcessCharacterActionCombatEvent(IImmutableList<DungeonBot> dungeonBots, int combatTimer, IImmutableList<Enemy> enemies, List<ActionResult> actionResults, List<CombatEvent> newCombatEvents, CombatEvent combatEvent)
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

                        actionResults.Add(new ActionResult(combatTimer, combatEvent.Character, $"{combatEvent.Character.Name} is stunned.", null, CreateCharacterList(dungeonBots, enemies).Select(c => new CharacterRecord(c.Id, c.Name, c.MaximumHealth, c.CurrentHealth, c is DungeonBot)).ToImmutableList(), ImmutableList.Create<CombatEvent>()));
                        newCombatEvents.Add(combatEvent with { CombatTime = combatTimer + startOfCharacterActionCombatEffect.Value });

                        combatEvent.Character.CombatEffects.Remove(startOfCharacterActionCombatEffect);

                        return;
                }
            }

            var actionComponent = new ActionComponent(combatEvent.Character);

            var sensorComponent = new SensorComponent(
                dungeonBots.Where(d => d.CurrentHealth > 0).Cast<IDungeonBot>().ToImmutableList(),
                enemies.Where(e => e.CurrentHealth > 0).Cast<IEnemy>().ToImmutableList(),
                combatTimer,
                actionResults.Cast<IActionResult>().ToImmutableList());

            var action = combatEvent.Character switch
            {
                DungeonBot dungeonBot => await _actionModuleExecuter.ExecuteActionModule(dungeonBot, actionComponent, sensorComponent),
                Enemy enemy => await _actionModuleExecuter.ExecuteEnemyActionModule(enemy, actionComponent, sensorComponent),
                _ => throw new UnknownCharacterTypeException($"Unknown Character Type: {combatEvent.Character.GetType()}"),
            };

            var newActionResults = _combatActionProcessor.ProcessAction(action, combatEvent.Character, combatTimer, CreateCharacterList(dungeonBots, enemies));

            newCombatEvents.AddRange(newActionResults.SelectMany(a => a.NewCombatEvents));

            actionResults.AddRange(newActionResults);

            newCombatEvents.Add(combatEvent with { CombatTime = combatTimer + _combatValueCalculator.GetIterationsUntilNextAction(combatEvent.Character) });
        }

        private static IImmutableList<CharacterBase> CreateCharacterList(IImmutableList<DungeonBot> dungeonBots, IImmutableList<Enemy> enemies)
        {
            var characterList = new List<CharacterBase>();

            characterList.AddRange(dungeonBots);
            characterList.AddRange(enemies);

            return characterList.ToImmutableList();
        }
    }
}
