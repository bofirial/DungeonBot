using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using DungeonBotGame.Client.BusinessLogic.Combat;
using DungeonBotGame.Client.Store.Adventures;
using DungeonBotGame.Client.Store.DungeonBots;
using DungeonBotGame.Models.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DungeonBotGame.Tests
{
    public class MysticRepairBotLevelOneCombatTests : IClassFixture<DependencyInjectionFixture>
    {
        private readonly DependencyInjectionFixture _dependencyInjectionFixture;

        public MysticRepairBotLevelOneCombatTests(DependencyInjectionFixture dependencyInjectionFixture)
        {
            _dependencyInjectionFixture = dependencyInjectionFixture;
        }

        private static (AdventureState, DungeonBotState) GetInitialStates(ServiceProvider serviceProvider)
        {
            var adventureFeature = serviceProvider.GetService<AdventureFeature>();
            var dungeonBotFeature = serviceProvider.GetService<DungeonBotFeature>();

            return ((AdventureState)adventureFeature.GetState(), (DungeonBotState)dungeonBotFeature.GetState());
        }

        private static DungeonBotViewModel GetLevelOneMysticRepairBot(DungeonBotState initialDungeonBotState) =>
            initialDungeonBotState.DungeonBots.First(d => d.DungeonBotClass == DungeonBotClass.MysticRepairBot && d.Level == 1);

        [Fact]
        public async Task RatAdventure_WithAttackOnlyScript_Succeeds()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Rat")));
            var dungeonBot = GetLevelOneMysticRepairBot(initialDungeonBotState);

            var runAdventureAction = new RunAdventureAction(adventure, ImmutableList.Create(dungeonBot), Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task DragonAdventure_WithAttackOnlyScript_Fails()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Dragon")));
            var dungeonBot = GetLevelOneMysticRepairBot(initialDungeonBotState);

            var runAdventureAction = new RunAdventureAction(adventure, ImmutableList.Create(dungeonBot), Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task DragonAdventure_WithRepairScript_Succeeds()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Dragon")));
            var dungeonBot = GetLevelOneMysticRepairBot(initialDungeonBotState) with
            {
                ActionModuleFiles = ImmutableList.Create(new ActionModuleFileViewModel("DungeonBot001.cs", @"using System;
using System.Linq;
using System.Threading.Tasks;
using DungeonBotGame;

namespace DungeonBotGame.Scripts
{
    public class DungeonBotActionModule
    {
        [ActionModuleEntrypoint]
        public IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent)
        {
            var dungeonBot = sensorComponent.DungeonBots.First();
            var enemy = sensorComponent.Enemies.First();

            if (actionComponent.RepairIsAvailable() && dungeonBot.CurrentHealth < 50)
            {
                return actionComponent.UseRepair(dungeonBot);
            }

            return actionComponent.Attack(enemy);
        }
    }
}"))
            };

            var runAdventureAction = new RunAdventureAction(adventure, ImmutableList.Create(dungeonBot), Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task WolfAdventure_WithAttackOnlyScript_Fails()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Wolf")));
            var dungeonBot = GetLevelOneMysticRepairBot(initialDungeonBotState);

            var runAdventureAction = new RunAdventureAction(adventure, ImmutableList.Create(dungeonBot), Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task WolfAdventure_WithRepairScript_Fails()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Wolf")));
            var dungeonBot = GetLevelOneMysticRepairBot(initialDungeonBotState) with
            {
                ActionModuleFiles = ImmutableList.Create(new ActionModuleFileViewModel("DungeonBot001.cs", @"using System;
using System.Linq;
using System.Threading.Tasks;
using DungeonBotGame;

namespace DungeonBotGame.Scripts
{
    public class DungeonBotActionModule
    {
        [ActionModuleEntrypoint]
        public IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent)
        {
            var dungeonBot = sensorComponent.DungeonBots.First();
            var enemy = sensorComponent.Enemies.First();

            if (actionComponent.RepairIsAvailable() && dungeonBot.CurrentHealth < 50)
            {
                return actionComponent.UseRepair(dungeonBot);
            }

            return actionComponent.Attack(enemy);
        }
    }
}"))
            };

            var runAdventureAction = new RunAdventureAction(adventure, ImmutableList.Create(dungeonBot), Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task TrollAdventure_WithAttackOnlyScript_Fails()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Troll")));
            var dungeonBot = GetLevelOneMysticRepairBot(initialDungeonBotState);

            var runAdventureAction = new RunAdventureAction(adventure, ImmutableList.Create(dungeonBot), Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task TrollAdventure_WithRepairScript_Succeeds()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Troll")));
            var dungeonBot = GetLevelOneMysticRepairBot(initialDungeonBotState) with
            {
                ActionModuleFiles = ImmutableList.Create(new ActionModuleFileViewModel("DungeonBot001.cs", @"using System;
using System.Linq;
using System.Threading.Tasks;
using DungeonBotGame;

namespace DungeonBotGame.Scripts
{
    public class DungeonBotActionModule
    {
        [ActionModuleEntrypoint]
        public IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent)
        {
            var dungeonBot = sensorComponent.DungeonBots.First();
            var enemy = sensorComponent.Enemies.First();

            if (actionComponent.RepairIsAvailable() && dungeonBot.CurrentHealth < 50)
            {
                return actionComponent.UseRepair(dungeonBot);
            }

            return actionComponent.Attack(enemy);
        }
    }
}"))
            };

            var runAdventureAction = new RunAdventureAction(adventure, ImmutableList.Create(dungeonBot), Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task BatAdventure_WithAttackOnlyScript_Fails()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Bat")));
            var dungeonBot = GetLevelOneMysticRepairBot(initialDungeonBotState);

            var runAdventureAction = new RunAdventureAction(adventure, ImmutableList.Create(dungeonBot), Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task BatAdventure_WithRepairScript_Succeeds()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Bat")));
            var dungeonBot = GetLevelOneMysticRepairBot(initialDungeonBotState) with
            {
                ActionModuleFiles = ImmutableList.Create(new ActionModuleFileViewModel("DungeonBot001.cs", @"using System;
using System.Linq;
using System.Threading.Tasks;
using DungeonBotGame;

namespace DungeonBotGame.Scripts
{
    public class DungeonBotActionModule
    {
        [ActionModuleEntrypoint]
        public IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent)
        {
            var dungeonBot = sensorComponent.DungeonBots.First();
            var enemy = sensorComponent.Enemies.First();

            if (actionComponent.RepairIsAvailable() && dungeonBot.CurrentHealth < 50)
            {
                return actionComponent.UseRepair(dungeonBot);
            }

            return actionComponent.Attack(enemy);
        }
    }
}"))
            };

            var runAdventureAction = new RunAdventureAction(adventure, ImmutableList.Create(dungeonBot), Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.True(result.Success);
        }
    }
}
