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
    public class WarriorBotLevelOneCombatTests : IClassFixture<DependencyInjectionFixture>
    {
        private readonly DependencyInjectionFixture _dependencyInjectionFixture;

        public WarriorBotLevelOneCombatTests(DependencyInjectionFixture dependencyInjectionFixture)
        {
            _dependencyInjectionFixture = dependencyInjectionFixture;
        }

        private static (AdventureState, DungeonBotState) GetInitialStates(ServiceProvider serviceProvider)
        {
            var adventureFeature = serviceProvider.GetService<AdventureFeature>();
            var dungeonBotFeature = serviceProvider.GetService<DungeonBotFeature>();

            return ((AdventureState)adventureFeature.GetState(), (DungeonBotState)dungeonBotFeature.GetState());
        }

        private static DungeonBotViewModel GetLevelOneWarriorBot(DungeonBotState initialDungeonBotState) =>
            initialDungeonBotState.DungeonBots.First(d => d.DungeonBotClass == DungeonBotClass.WarriorBot && d.Level == 1);

        [Fact]
        public async Task RatAdventure_WithAttackOnlyScript_Succeeds()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Rat")));
            var dungeonBot = GetLevelOneWarriorBot(initialDungeonBotState);

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
            var dungeonBot = GetLevelOneWarriorBot(initialDungeonBotState);

            var runAdventureAction = new RunAdventureAction(adventure, ImmutableList.Create(dungeonBot), Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task DragonAdventure_WithHeavySwingScript_Succeeds()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Dragon")));
            var dungeonBot = GetLevelOneWarriorBot(initialDungeonBotState) with
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
            var enemy = sensorComponent.Enemies.First();

            if (actionComponent.HeavySwingIsAvailable())
            {
                return actionComponent.UseHeavySwing(enemy);
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
            var dungeonBot = GetLevelOneWarriorBot(initialDungeonBotState);

            var runAdventureAction = new RunAdventureAction(adventure, ImmutableList.Create(dungeonBot), Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task WolfAdventure_WithHeavySwingScript_Fails()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Wolf")));
            var dungeonBot = GetLevelOneWarriorBot(initialDungeonBotState) with
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
            var enemy = sensorComponent.Enemies.First();

            if (actionComponent.HeavySwingIsAvailable())
            {
                return actionComponent.UseHeavySwing(enemy);
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
        public async Task WolfAdventure_WithFinishHimScript_Succeeds()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Wolf")));
            var dungeonBot = GetLevelOneWarriorBot(initialDungeonBotState) with
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
            var enemy = sensorComponent.Enemies.First();

            if (actionComponent.HeavySwingIsAvailable() && enemy.CurrentHealth < 60)
            {
                return actionComponent.UseHeavySwing(enemy);
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
        public async Task TrollAdventure_WithAttackOnlyScript_Fails()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Troll")));
            var dungeonBot = GetLevelOneWarriorBot(initialDungeonBotState);

            var runAdventureAction = new RunAdventureAction(adventure, ImmutableList.Create(dungeonBot), Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task TrollAdventure_WithHeavySwingScript_Succeeds()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Troll")));
            var dungeonBot = GetLevelOneWarriorBot(initialDungeonBotState) with
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
            var enemy = sensorComponent.Enemies.First();

            if (actionComponent.HeavySwingIsAvailable())
            {
                return actionComponent.UseHeavySwing(enemy);
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
            var dungeonBot = GetLevelOneWarriorBot(initialDungeonBotState);

            var runAdventureAction = new RunAdventureAction(adventure, ImmutableList.Create(dungeonBot), Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task BatAdventure_WithHeavySwingScript_Fails()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Bat")));
            var dungeonBot = GetLevelOneWarriorBot(initialDungeonBotState) with
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
            var enemy = sensorComponent.Enemies.First();

            if (actionComponent.HeavySwingIsAvailable())
            {
                return actionComponent.UseHeavySwing(enemy);
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
        public async Task BatAdventure_FocusAttacksScript_Succeeds()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Bat")));
            var dungeonBot = GetLevelOneWarriorBot(initialDungeonBotState) with
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
            var heavySwingEnemy = sensorComponent.Enemies.First();
            var attackEnemy = sensorComponent.Enemies.Last();

            if (actionComponent.HeavySwingIsAvailable())
            {
                return actionComponent.UseHeavySwing(heavySwingEnemy);
            }

            return actionComponent.Attack(attackEnemy);
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
