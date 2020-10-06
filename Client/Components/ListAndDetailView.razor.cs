using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace DungeonBot.Client.Components
{
    public partial class ListAndDetailView<TListItem> where TListItem : class
    {
        [Parameter]
        public IEnumerable<TListItem>? List { get; set; }

        [Parameter]
        public Func<TListItem, string>? KeyFunction { get; set; }

        [Parameter]
        public RenderFragment<TListItem>? ListItemView { get; set; }

        [Parameter]
        public RenderFragment<TListItem>? DetailView { get; set; }

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
}
