using System.Collections.Immutable;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Models.Combat
{
    public interface IEnemyActionModule
    {
        IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent);

        IImmutableList<ActionModuleFileViewModel> SourceCodeFiles { get; }
    }
}
