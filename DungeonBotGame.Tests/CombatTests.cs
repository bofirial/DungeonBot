using Microsoft.Extensions.DependencyInjection;
using Xunit;
using DungeonBotGame.Client.BusinessLogic.Combat;
using System.Threading.Tasks;
using DungeonBotGame.Client.Store.Adventures;
using DungeonBotGame.Client.Store.DungeonBots;
using System.Linq;
using System;
using System.Collections.Immutable;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Tests
{
    public class InitialCombatTests : IClassFixture<DependencyInjectionFixture>
    {
        private readonly DependencyInjectionFixture _dependencyInjectionFixture;

        public InitialCombatTests(DependencyInjectionFixture dependencyInjectionFixture)
        {
            _dependencyInjectionFixture = dependencyInjectionFixture;
        }

        private static (AdventureState, DungeonBotState) GetInitialStates(ServiceProvider serviceProvider)
        {
            var adventureFeature = serviceProvider.GetService<AdventureFeature>();
            var dungeonBotFeature = serviceProvider.GetService<DungeonBotFeature>();

            return ((AdventureState)adventureFeature.GetState(), (DungeonBotState)dungeonBotFeature.GetState());
        }

        [Fact]
        public async Task RatAdventure_WithAttackOnlyScript_Succeeds()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Rat")));
            var dungeonBot = initialDungeonBotState.DungeonBots[0];

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
            var dungeonBot = initialDungeonBotState.DungeonBots[0];

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
            var dungeonBot = initialDungeonBotState.DungeonBots[0] with
            {
                ActionModuleFiles = ImmutableList.Create(new ActionModuleFileViewModel("DungeonBot001.cs", @"using System;
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
            var dungeonBot = initialDungeonBotState.DungeonBots[0];

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
            var dungeonBot = initialDungeonBotState.DungeonBots[0] with
            {
                ActionModuleFiles = ImmutableList.Create(new ActionModuleFileViewModel("DungeonBot001.cs", @"using System;
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
            var dungeonBot = initialDungeonBotState.DungeonBots[0] with
            {
                ActionModuleFiles = ImmutableList.Create(new ActionModuleFileViewModel("DungeonBot001.cs", @"using System;
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
            var dungeonBot = initialDungeonBotState.DungeonBots[0];

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
            var dungeonBot = initialDungeonBotState.DungeonBots[0] with
            {
                ActionModuleFiles = ImmutableList.Create(new ActionModuleFileViewModel("DungeonBot001.cs", @"using System;
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
            var dungeonBot = initialDungeonBotState.DungeonBots[0];

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
            var dungeonBot = initialDungeonBotState.DungeonBots[0] with
            {
                ActionModuleFiles = ImmutableList.Create(new ActionModuleFileViewModel("DungeonBot001.cs", @"using System;
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
            var dungeonBot = initialDungeonBotState.DungeonBots[0] with
            {
                ActionModuleFiles = ImmutableList.Create(new ActionModuleFileViewModel("DungeonBot001.cs", @"using System;
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
