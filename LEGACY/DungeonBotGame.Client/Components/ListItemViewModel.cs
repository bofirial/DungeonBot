namespace DungeonBotGame.Client.Components
{
    public record ListItemViewModel<TListItem>(TListItem ListItem, bool IsCurrentListItem) where TListItem : class;
}
