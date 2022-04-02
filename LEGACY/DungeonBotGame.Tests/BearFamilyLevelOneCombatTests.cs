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
    public class BearFamilyLevelOneCombatTests : IClassFixture<DependencyInjectionFixture>
    {
        private readonly DependencyInjectionFixture _dependencyInjectionFixture;

        public BearFamilyLevelOneCombatTests(DependencyInjectionFixture dependencyInjectionFixture)
        {
            _dependencyInjectionFixture = dependencyInjectionFixture;
        }

        private static (AdventureState, DungeonBotState) GetInitialStates(ServiceProvider serviceProvider)
        {
            var adventureFeature = serviceProvider.GetService<AdventureFeature>();
            var dungeonBotFeature = serviceProvider.GetService<DungeonBotFeature>();

            return ((AdventureState)adventureFeature.GetState(), (DungeonBotState)dungeonBotFeature.GetState());
        }

        private static (DungeonBotViewModel WarriorBot, DungeonBotViewModel MysticRepairBot) GetLevelOneWarriorBotAndMysticRepairBot(DungeonBotState initialDungeonBotState) =>
            (
                WarriorBot: initialDungeonBotState.DungeonBots.First(d => d.DungeonBotClass == DungeonBotClass.WarriorBot && d.Level == 1),
                MysticRepairBot: initialDungeonBotState.DungeonBots.First(d => d.DungeonBotClass == DungeonBotClass.MysticRepairBot && d.Level == 1)
            );

        [Fact]
        public async Task BearFamilyAdventure_WithAttackOnlyScripts_Fails()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Bear")));
            (var warriorBot, var mysticRepairBot) = GetLevelOneWarriorBotAndMysticRepairBot(initialDungeonBotState);

            var dungeonBots = ImmutableList.Create(warriorBot, mysticRepairBot);

            var runAdventureAction = new RunAdventureAction(adventure, dungeonBots, Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task BearFamilyAdventure_WithoutRepair_Fails()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Bear")));
            (var warriorBot, var mysticRepairBot) = GetLevelOneWarriorBotAndMysticRepairBot(initialDungeonBotState);

            warriorBot = warriorBot with
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
            var enemy = sensorComponent.Enemies.FirstOrDefault(e => e.Name == ""Mama Bear"");

            if (enemy == null)
            {
                enemy = sensorComponent.Enemies.First();
            }

            if (actionComponent.HeavySwingIsAvailable())
            {
                return actionComponent.UseHeavySwing(enemy);
            }

            return actionComponent.Attack(enemy);
        }
    }
}"))
            };

            mysticRepairBot = mysticRepairBot with
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
            var enemy = sensorComponent.Enemies.FirstOrDefault(e => e.Name == ""Mama Bear"");

            if (enemy == null)
            {
                enemy = sensorComponent.Enemies.First();
            }

            return actionComponent.Attack(enemy);
        }
    }
}"))
            };
            var dungeonBots = ImmutableList.Create(warriorBot, mysticRepairBot);

            var runAdventureAction = new RunAdventureAction(adventure, dungeonBots, Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task BearFamilyAdventure_WithoutHeavySwing_Fails()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Bear")));
            (var warriorBot, var mysticRepairBot) = GetLevelOneWarriorBotAndMysticRepairBot(initialDungeonBotState);

            warriorBot = warriorBot with
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
            var enemy = sensorComponent.Enemies.FirstOrDefault(e => e.Name == ""Mama Bear"");

            if (enemy == null)
            {
                enemy = sensorComponent.Enemies.First();
            }

            return actionComponent.Attack(enemy);
        }
    }
}"))
            };

            mysticRepairBot = mysticRepairBot with
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
            foreach (var dungeonBot in sensorComponent.DungeonBots.OrderBy(d => d.CurrentHealth))
            {
                if (actionComponent.RepairIsAvailable() && dungeonBot.CurrentHealth < dungeonBot.MaximumHealth)
                {
                    return actionComponent.UseRepair(dungeonBot);
                }
            }

            var enemy = sensorComponent.Enemies.FirstOrDefault(e => e.Name == ""Mama Bear"");

            if (enemy == null)
            {
                enemy = sensorComponent.Enemies.First();
            }

            return actionComponent.Attack(enemy);
        }
    }
}"))
            };
            var dungeonBots = ImmutableList.Create(warriorBot, mysticRepairBot);

            var runAdventureAction = new RunAdventureAction(adventure, dungeonBots, Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task BearFamilyAdventure_WithoutTargettingMamaBear_Fails()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Bear")));
            (var warriorBot, var mysticRepairBot) = GetLevelOneWarriorBotAndMysticRepairBot(initialDungeonBotState);

            warriorBot = warriorBot with
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

            mysticRepairBot = mysticRepairBot with
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
            foreach (var dungeonBot in sensorComponent.DungeonBots.OrderBy(d => d.CurrentHealth))
            {
                if (actionComponent.RepairIsAvailable() && dungeonBot.CurrentHealth < dungeonBot.MaximumHealth)
                {
                    return actionComponent.UseRepair(dungeonBot);
                }
            }

            var enemy = sensorComponent.Enemies.First();

            return actionComponent.Attack(enemy);
        }
    }
}"))
            };
            var dungeonBots = ImmutableList.Create(warriorBot, mysticRepairBot);

            var runAdventureAction = new RunAdventureAction(adventure, dungeonBots, Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task BearFamilyAdventure_WithoutOrderingByHealth_Fails()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Bear")));
            (var warriorBot, var mysticRepairBot) = GetLevelOneWarriorBotAndMysticRepairBot(initialDungeonBotState);

            warriorBot = warriorBot with
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
            var enemy = sensorComponent.Enemies.FirstOrDefault(e => e.Name == ""Mama Bear"");

            if (enemy == null)
            {
                enemy = sensorComponent.Enemies.First();
            }

            if (actionComponent.HeavySwingIsAvailable())
            {
                return actionComponent.UseHeavySwing(enemy);
            }

            return actionComponent.Attack(enemy);
        }
    }
}"))
            };

            mysticRepairBot = mysticRepairBot with
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
            foreach (var dungeonBot in sensorComponent.DungeonBots)
            {
                if (actionComponent.RepairIsAvailable() && dungeonBot.CurrentHealth < dungeonBot.MaximumHealth)
                {
                    return actionComponent.UseRepair(dungeonBot);
                }
            }

            var enemy = sensorComponent.Enemies.FirstOrDefault(e => e.Name == ""Mama Bear"");

            if (enemy == null)
            {
                enemy = sensorComponent.Enemies.First();
            }

            return actionComponent.Attack(enemy);
        }
    }
}"))
            };
            var dungeonBots = ImmutableList.Create(warriorBot, mysticRepairBot);

            var runAdventureAction = new RunAdventureAction(adventure, dungeonBots, Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task BearFamilyAdventure_WithTargetMamaBearAndUseAbilitiesScripts_Succeeds()
        {
            //Arrange
            var serviceProvider = _dependencyInjectionFixture.ServiceProvider;
            (var initialAdventureState, var initialDungeonBotState) = GetInitialStates(serviceProvider);

            var adventureRunner = serviceProvider.GetService<IAdventureRunner>();

            var adventure = initialAdventureState.Adventures.First(a => a.Encounters.Any(e => e.Name.Contains("Bear")));
            (var warriorBot, var mysticRepairBot) = GetLevelOneWarriorBotAndMysticRepairBot(initialDungeonBotState);

            warriorBot = warriorBot with
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
            var enemy = sensorComponent.Enemies.FirstOrDefault(e => e.Name == ""Mama Bear"");

            if (enemy == null)
            {
                enemy = sensorComponent.Enemies.First();
            }

            if (actionComponent.HeavySwingIsAvailable())
            {
                return actionComponent.UseHeavySwing(enemy);
            }

            return actionComponent.Attack(enemy);
        }
    }
}"))
            };

            mysticRepairBot = mysticRepairBot with
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
            foreach (var dungeonBot in sensorComponent.DungeonBots.OrderBy(d => d.CurrentHealth))
            {
                if (actionComponent.RepairIsAvailable() && dungeonBot.CurrentHealth < dungeonBot.MaximumHealth)
                {
                    return actionComponent.UseRepair(dungeonBot);
                }
            }

            var enemy = sensorComponent.Enemies.FirstOrDefault(e => e.Name == ""Mama Bear"");

            if (enemy == null)
            {
                enemy = sensorComponent.Enemies.First();
            }

            return actionComponent.Attack(enemy);
        }
    }
}"))
            };
            var dungeonBots = ImmutableList.Create(warriorBot, mysticRepairBot);

            var runAdventureAction = new RunAdventureAction(adventure, dungeonBots, Guid.NewGuid().ToString());

            //Act
            var result = await adventureRunner.RunAdventureAsync(runAdventureAction);

            //Assert
            Assert.True(result.Success);
        }
    }
}
