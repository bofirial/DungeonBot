using DungeonBotGame.Combat;
using Microsoft.AspNetCore.Components;

namespace DungeonBotGame.Components;
public partial class AdventureHistoryModal
{
    [Parameter]
    public AdventureHistory? AdventureHistory { get; set; }

    public Modal? Modal { get; set; }

    public async Task CloseAsync()
    {
        if (Modal != null)
        {
            await Modal.CloseAsync();
        }
    }

    public async Task ShowAsync()
    {
        if (Modal != null)
        {
            await Modal.ShowAsync();
        }
    }

    public string GetCombatLogText(IAction action)
    {
        return action switch
        {
            AdventureStartAction => "The adventure began.",
            MoveAction moveAction => $"{moveAction.Character.Name} moved to ({moveAction.Location.X}, {moveAction.Location.Y}).",
            InteractAction interactAction => interactAction.Target switch
            {
                TreasureChest => $"{interactAction.Character.Name} opened the treasure chest at ({interactAction.Target.Location.X}, {interactAction.Target.Location.Y}).",
                AdventureExit => $"{interactAction.Character.Name} exited the adventure.",
                _ => $"{interactAction.Character.Name} interacted with the {interactAction.Target.GetType()} at ({interactAction.Target.Location.X}, {interactAction.Target.Location.Y}).",
            },
            _ => string.Empty
        };
    }
}
