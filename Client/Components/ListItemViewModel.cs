namespace DungeonBot.Client.Components
{
    public class ListItemViewModel<TListItem> where TListItem : class
    {
        public TListItem? ListItem { get; set; }

        public bool IsCurrentListItem { get; set; }
    }
}
