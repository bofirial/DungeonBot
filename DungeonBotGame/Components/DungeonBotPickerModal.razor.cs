using DungeonBotGame.Data;
using Microsoft.AspNetCore.Components;

namespace DungeonBotGame.Components;

public partial class DungeonBotPickerModal
{
    [Parameter]
    public IEnumerable<DungeonBot> DungeonBots { get; set; } = Array.Empty<DungeonBot>();

    public Action<DungeonBot> DungeonBotSelectionAction { get; set; } = dungeonBot => { };

    public DungeonBot? SelectedDungeonBot { get; set; }

    public IEnumerable<DungeonBot> UnavailableDungeonBots { get; set; } = Array.Empty<DungeonBot>();

    public Modal? Modal { get; set; }

    public async Task SelectAsync(DungeonBot dungeonBot)
    {
        DungeonBotSelectionAction(dungeonBot);

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
}
