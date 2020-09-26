using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace DungeonBot.Client.Components
{
    public partial class ListAndDetailView<TListItem> where TListItem : class
    {
        [Parameter]
        public IEnumerable<TListItem> List { get; set; } = Array.Empty<TListItem>();

        [Parameter]
        public RenderFragment<TListItem>? ListItemView { get; set; }

        [Parameter]
        public RenderFragment<TListItem>? DetailView { get; set; }

        private TListItem? CurrentListItem { get; set; }

        private void SetActiveListItem(TListItem listItem) => CurrentListItem = listItem;
    }
}
