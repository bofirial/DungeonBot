﻿using System;
using System.Collections.Generic;
using DungeonBotGame.Models.ViewModels;
using Fluxor;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public class DungeonBotFeature : Feature<DungeonBotState>
    {
        private const string DefaultActionModule = @"using System;
using System.Threading.Tasks;
using DungeonBotGame;

namespace DungeonBotGame.Scripts
{
    public class AttackActionModule
    {
        [ActionModuleEntrypoint]
        public IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent)
        {
            // if (actionComponent.HeavySwingIsAvailable())
            // {
            //     return actionComponent.UseHeavySwing(sensorComponent.Enemy);
            // }

            return actionComponent.Attack(sensorComponent.Enemy);
        }
    }
}";

        public override string GetName() => nameof(DungeonBotState);

        protected override DungeonBotState GetInitialState()
        {
            return new DungeonBotState(new List<DungeonBotViewModel>()
            {
                new DungeonBotViewModel(
                    Guid.NewGuid().ToString(),
                    "DungeonBot001",
                    "/images/temp/dungeonbot.png",
                    new ActionModuleLibraryViewModel("DungeonBot001", Array.Empty<byte>(), new ActionModuleFileViewModel("DungeonBotGame.cs", DefaultActionModule)),
                    new List<AbilityType>()
                    {
                        AbilityType.HeavySwing
                    })
            });
        }
    }
}