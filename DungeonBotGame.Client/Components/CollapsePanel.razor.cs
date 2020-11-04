using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DungeonBotGame.Client.Components
{
    public partial class CollapsePanel
    {
        [Parameter]
        public string HeadingId { get; set; } = string.Empty;

        [Parameter]
        public string CollapseSectionId { get; set; } = string.Empty;

        [Parameter]
        public bool IsCollapsedInitially { get; set; }

        [Parameter]
        public string? ParentAccordionId { get; set; }

        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        [Parameter]
        public RenderFragment? HeadingView { get; set; }

        [Parameter]
        public RenderFragment? CollapseSectionView { get; set; }

        [Parameter]
        public EventCallback OnShown { get; set; }

        [Parameter]
        public EventCallback OnShow { get; set; }

        [Parameter]
        public EventCallback OnHidden { get; set; }

        [Parameter]
        public EventCallback OnHide { get; set; }

        [Inject]
        public IJSRuntime? JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(CollapseSectionId) && JSRuntime != null && (OnShown.HasDelegate || OnShow.HasDelegate || OnHidden.HasDelegate || OnHide.HasDelegate))
            {
                await JSRuntime.InvokeVoidAsync("registerCollapsePanelEvents", CollapseSectionId, DotNetObjectReference.Create(this));
            }

            await base.OnInitializedAsync();
        }

        [JSInvokable]
        public async Task TriggerEventAsync(string collapsePanelEvent)
        {
            System.Console.WriteLine($"Event Triggered: {collapsePanelEvent}");
            switch (collapsePanelEvent)
            {
                case "shown.bs.collapse":
                    await OnShown.InvokeAsync();
                    break;
                case "show.bs.collapse":
                    await OnShow.InvokeAsync();
                    break;
                case "hidden.bs.collapse":
                    await OnHidden.InvokeAsync();
                    break;
                case "hide.bs.collapse":
                    await OnHide.InvokeAsync();
                    break;
            }
        }
    }
}
