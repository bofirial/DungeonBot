using System;
using System.Linq;
using System.Reflection;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic.Compilation
{
    public interface IActionModuleContextProvider
    {
        ActionModuleContext GetActionModuleContext(ActionModuleLibraryViewModel actionModuleLibrary);
    }

    public class ActionModuleContextProvider : IActionModuleContextProvider
    {
        public ActionModuleContext GetActionModuleContext(ActionModuleLibraryViewModel actionModuleLibrary)
        {
            var assembly = Assembly.Load(actionModuleLibrary.Assembly.ToArray());

            var methods = assembly.GetTypes().SelectMany(t => t.GetMethods()).Where(m => m.GetCustomAttributes(typeof(ActionModuleEntrypointAttribute), false).Length > 0);

            //TODO: Consider Replacing this Reflection with Source Generator(s) and Analyzer(s)

            //TODO: Error for multiple entry points
            //TODO: Error for no entry points
            //TODO: Error for invalid method parameters
            //TODO: Error for invalid method return type
            //TODO: Error for ActionModule has no parameterless constructor

            var actionMethod = methods.First();

            var type = actionMethod.DeclaringType;

            if (type == null)
            {
                throw new Exception("Failed to get DeclaringType.");
            }

            var actionModule = Activator.CreateInstance(type);

            return new ActionModuleContext(actionModule, actionMethod);
        }
    }
}
