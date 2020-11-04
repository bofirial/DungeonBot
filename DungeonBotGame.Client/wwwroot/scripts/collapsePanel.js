const collapsePanelEvents = ['shown.bs.collapse', 'show.bs.collapse', 'hidden.bs.collapse', 'hide.bs.collapse'];
window.registerCollapsePanelEvents = (collapsePanelId, collapsePanelComponent) => {
    window.setTimeout(() => {
        var targetCollapsePanel = document.getElementById(collapsePanelId);
        for (const collapsePanelEvent of collapsePanelEvents) {
            console.log('Event Target Element ID: ' + collapsePanelId);
            console.log('Target Element:', targetCollapsePanel);
            if (targetCollapsePanel) {
                targetCollapsePanel.addEventListener(collapsePanelEvent, function () {
                    console.log('Event Triggered (JS): ' + collapsePanelEvent);
                    collapsePanelComponent.invokeMethodAsync('TriggerEventAsync', collapsePanelEvent);
                });
            }
        }
    }, 1);
};
export {};
//# sourceMappingURL=collapsePanel.js.map