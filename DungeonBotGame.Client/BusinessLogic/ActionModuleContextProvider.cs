using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.Models.Display;

namespace DungeonBotGame.Client.BusinessLogic
{
    public class ActionModuleContextProvider : IActionModuleContextProvider
    {
        public Task<ActionModuleContext> GetActionModuleContext(ActionModuleLibraryViewModel actionModuleLibrary)
        {
            var assembly = Assembly.Load(actionModuleLibrary.Assembly.ToArray());

            var methods = assembly.GetTypes().SelectMany(t => t.GetMethods()).Where(m => m.GetCustomAttributes(typeof(ActionModuleEntrypointAttribute), false).Length > 0);

            //TODO: Error for multiple entry points
            //TODO: Error for no entry points
            //TODO: Error for invalid method parameters
            //TODO: Error for invalid method return type
            //TODO: Error for ActionModule has no parameterless constructor

            var actionMethod = methods.First();

            var type = actionMethod.DeclaringType;

            var actionModule = Activator.CreateInstance(type);

            return Task.FromResult(new ActionModuleContext()
            {
                ActionModuleObject = actionModule,
                ActionModuleEntryPointMethodInfo = actionMethod
            });
        }
    }
}
