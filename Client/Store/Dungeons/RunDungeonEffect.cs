using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DungeonBot.Models.Combat;
using DungeonBot.Models.Display;
using Fluxor;

namespace DungeonBot.Client.Store.Dungeons
{
    public class RunDungeonEffect : Effect<RunDungeonAction>
    {
        protected override Task HandleAsync(RunDungeonAction action, IDispatcher dispatcher)
        {
            var assembly = Assembly.Load(action.ActionModuleLibrary.Assembly.ToArray());

            var methods = assembly.GetTypes().SelectMany(t => t.GetMethods()).Where(m => m.GetCustomAttributes(typeof(ActionModuleEntrypointAttribute), false).Length > 0);

            //TODO: Error for multiple entry points
            //TODO: Error for entry point not found
            //TODO: Error for invalid method parameters
            //TODO: Error for invalid method return type

            var actionMethod = methods.First();

            var type = actionMethod.DeclaringType;

            var actionModule = Activator.CreateInstance(type);

            var dungeonBot = new Player(action.ActionModuleLibrary.Name, 100);
            var enemy = new Enemy(action.Dungeon.Encounters.First().Name, 80);

            var actionComponent = new ActionComponent();
            var sensorComponent = new SensorComponent(enemy);

            while (dungeonBot.CurrentHealth > 0 && enemy.CurrentHealth > 0)
            {
                dungeonBot.CurrentHealth -= 10;

                var parameters = new object?[] { actionComponent, sensorComponent };

                var result = (IAction)actionMethod.Invoke(actionModule, parameters);

                if (result.ActionType == ActionType.Attack)
                {
                    enemy.CurrentHealth -= 10;
                }
            }

            dispatcher.Dispatch(new DungeonResultAction(action.Dungeon, new DungeonResult(action.RunId, enemy.CurrentHealth <= 0)));

            return Task.CompletedTask;
        }
    }
}
