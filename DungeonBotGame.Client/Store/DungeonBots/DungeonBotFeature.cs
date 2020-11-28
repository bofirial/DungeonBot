using System;
using System.Collections.Immutable;
using DungeonBotGame.Models.ViewModels;
using Fluxor;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public class DungeonBotFeature : Feature<DungeonBotState>
    {
        private const string DefaultActionModule = @"using System;
using System.Linq;
using System.Threading.Tasks;
using DungeonBotGame;

namespace DungeonBotGame.Scripts
{
    public class AttackActionModule
    {
        [ActionModuleEntrypoint]
        public IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent)
        {
            var enemy = sensorComponent.Enemies.First();

            // if (actionComponent.HeavySwingIsAvailable())
            // {
            //     return actionComponent.UseHeavySwing(enemy);
            // }

            // if (actionComponent.HeavySwingIsAvailable() && (enemy.Name != ""Wolf King"" || enemy.CurrentHealth <= 60))
            // {
            //     return actionComponent.UseHeavySwing(enemy);
            // }

            return actionComponent.Attack(enemy);
        }
    }
}";

        public override string GetName() => nameof(DungeonBotState);

        protected override DungeonBotState GetInitialState()
        {
            return new DungeonBotState(ImmutableList.Create(
                new DungeonBotViewModel(
                    Guid.NewGuid().ToString(),
                    "WarriorBot001",
                    DungeonBotClass.WarriorBot,
                    level: 1,
                    availableStatPoints: 0,
                    power: 5,
                    armor: 5,
                    speed: 5,
                    "/images/temp/dungeonbot.png",
                    ImmutableList.Create(new ActionModuleFileViewModel("WarriorBot001.cs", DefaultActionModule)),
                    ImmutableList.Create(AbilityType.HeavySwing),
                    actionModuleContext: null,
                    ImmutableList.Create<ErrorViewModel>()),

                new DungeonBotViewModel(
                    Guid.NewGuid().ToString(),
                    "Future WarriorBot001",
                    DungeonBotClass.WarriorBot,
                    level: 10,
                    availableStatPoints: 18,
                    power: 14,
                    armor: 14,
                    speed: 14,
                    "/images/temp/dungeonbot.png",
                    ImmutableList.Create(new ActionModuleFileViewModel("WarriorBot001.cs", DefaultActionModule)),
                    //ImmutableList.Create(AbilityType.HeavySwing), //TODO: Allow the User to select these abilities
                    ImmutableList.Create(AbilityType.HeavySwing, AbilityType.AnalyzeSituation, AbilityType.SurpriseAttack, AbilityType.SalvageStrikes),
                    actionModuleContext: null,
                    ImmutableList.Create<ErrorViewModel>()),

                new DungeonBotViewModel(
                    Guid.NewGuid().ToString(),
                    "MysticRepairBot001",
                    DungeonBotClass.MysticRepairBot,
                    level: 1,
                    availableStatPoints: 0,
                    power: 5,
                    armor: 5,
                    speed: 5,
                    "/images/temp/dungeonbot.png",
                    ImmutableList.Create(new ActionModuleFileViewModel("MysticRepairBot001.cs", DefaultActionModule)),
                    ImmutableList.Create<AbilityType>(), //TODO: Implement Heal
                    actionModuleContext: null,
                    ImmutableList.Create<ErrorViewModel>())
                ),
                false);
        }
    }
}
