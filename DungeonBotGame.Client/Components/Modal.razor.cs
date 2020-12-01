using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DungeonBotGame.Client.Components
{
    public partial class Modal
    {
        [Parameter]
        public string ModalId { get; set; } = string.Empty;

        [Parameter]
        public string ModalTitle { get; set; } = string.Empty;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public bool IsClosable { get; set; } = true;

        [Parameter]
        public string ModalDialogSize { get; set; } = string.Empty;

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
            if (JSRuntime != null && (OnShown.HasDelegate || OnShow.HasDelegate || OnHidden.HasDelegate || OnHide.HasDelegate))
            {
                await JSRuntime.InvokeVoidAsync("registerModalEvents", ModalId, DotNetObjectReference.Create(this));
            }

            await base.OnInitializedAsync();
        }

        [JSInvokable]
        public async Task TriggerEventAsync(string collapsePanelEvent)
        {
            switch (collapsePanelEvent)
            {
                case "shown.bs.modal":
                    await OnShown.InvokeAsync();
                    break;
                case "show.bs.modal":
                    await OnShow.InvokeAsync();
                    break;
                case "hidden.bs.modal":
                    await OnHidden.InvokeAsync();
                    break;
                case "hide.bs.modal":
                    await OnHide.InvokeAsync();
                    break;
            }
        }

        public async Task ShowAsync()
        {
            if (JSRuntime != null)
            {
                await JSRuntime.InvokeVoidAsync("launchModal", ModalId, IsClosable);
            }
        }

        public async Task CloseAsync()
        {
            if (JSRuntime != null)
            {
                await JSRuntime.InvokeVoidAsync("closeModal", ModalId);
            }
        }
    }
}
