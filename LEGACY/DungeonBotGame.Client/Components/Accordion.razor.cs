using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace DungeonBotGame.Client.Components
{
    public partial class Accordion<TListItem>
    {
        [Parameter]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public IEnumerable<TListItem>? List { get; set; }

        [Parameter]
        public Func<TListItem, string>? HeadingIdFunction { get; set; }

        [Parameter]
        public Func<TListItem, string>? CollapseSectionIdFunction { get; set; }

        [Parameter]
        public Func<TListItem, bool>? IsCollapsedInitiallyFunction { get; set; }

        [Parameter]
        public RenderFragment<TListItem>? HeadingView { get; set; }

        [Parameter]
        public RenderFragment<TListItem>? CollapseSectionView { get; set; }
    }
}
