
using System;
using System.Collections.Generic;
using DungeonBot.Client.BusinessLogic;
using DungeonBot.Models;
using Fluxor;

namespace DungeonBot.Client.Store.ActionModule
{
    public class ActionModuleFeature : Feature<ActionModuleState>
    {
        private const string DefaultActionModule = @"using System;
using System.Threading.Tasks;
using DungeonBot;

namespace DungeonBot.Scripts
{
    public class AttackActionModule
    {
        [ActionModuleEntrypoint]
        public IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent)
        {
            // return actionComponent.UseAbility(sensorComponent.Enemy, AbilityType.HeavySwing);
            return actionComponent.Attack(sensorComponent.Enemy);
        }
    }
}";
        private readonly ICSharpCompiler _cSharpCompiler;

        public ActionModuleFeature(ICSharpCompiler cSharpCompiler)
        {
            _cSharpCompiler = cSharpCompiler;
        }

        public override string GetName() => nameof(ActionModuleState);

        protected override ActionModuleState GetInitialState()
        {
            return new ActionModuleState(new List<ActionModuleLibrary>()
            {
                new ActionModuleLibrary(
                    "DungeonBot001",
                    Array.Empty<byte>(),
                    new ActionModuleFile("DungeonBot.cs", DefaultActionModule))
            });
        }
    }
}
