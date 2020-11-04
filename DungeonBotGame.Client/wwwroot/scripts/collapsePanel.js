const collapsePanelEvents = ['shown.bs.collapse', 'show.bs.collapse', 'hidden.bs.collapse', 'hide.bs.collapse'];
window.registerCollapsePanelEvents = (collapsePanelId, collapsePanelComponent) => {
    window.setTimeout(() => {
        var targetCollapsePanel = document.getElementById(collapsePanelId);
        for (const collapsePanelEvent of collapsePanelEvents) {
            if (targetCollapsePanel) {
                targetCollapsePanel.addEventListener(collapsePanelEvent, function () {
                    collapsePanelComponent.invokeMethodAsync('TriggerEventAsync', collapsePanelEvent);
                });
            }
        }
    }, 1);
};
export {};
//# sourceMappingURL=collapsePanel.js.map