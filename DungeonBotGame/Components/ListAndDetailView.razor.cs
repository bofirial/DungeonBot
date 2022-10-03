using Microsoft.AspNetCore.Components;

namespace DungeonBotGame.Components;

public record ListItemViewModel<TListItem>(TListItem ListItem, bool IsCurrentListItem);

public partial class ListAndDetailView<TListItem>
{
    [Parameter]
    public IEnumerable<TListItem>? List { get; set; }

    [Parameter]
    public Func<TListItem, string>? KeyFunction { get; set; }

    [Parameter]
    public RenderFragment<ListItemViewModel<TListItem>>? ListItemView { get; set; }

    [Parameter]
    public RenderFragment<TListItem>? DetailView { get; set; }

    [Parameter]
    public string SelectAnItemText { get; set; } = "Select an Item";

    private string CurrentListItemKey { get; set; } = string.Empty;

    private TListItem? CurrentListItem { get; set; }

    private void SetActiveListItem(TListItem listItem)
    {
        if (KeyFunction != null)
        {
            CurrentListItemKey = KeyFunction(listItem);
        }

        CurrentListItem = listItem;
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (KeyFunction != null && !string.IsNullOrEmpty(CurrentListItemKey) && CurrentListItem != null && List != null)
        {
            foreach (var listItem in List)
            {
                if (CurrentListItemKey == KeyFunction(listItem))
                {
                    CurrentListItem = listItem;
                }
            }
        }
    }
}
