export { }

declare const bootstrap: any;
declare global {
    interface Window {
        registerCollapsePanelEvents: any;
        closePanels: any;
    }
}

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

window.closePanels = (collapsePanelIds) => {
    for (var i in collapsePanelIds) {
        var collapsePanelId = collapsePanelIds[i];
        var targetCollapsePanel = document.getElementById(collapsePanelId);

        if (targetCollapsePanel) {
            var collapsePanel = bootstrap.Collapse.getInstance(targetCollapsePanel);

            if (collapsePanel) {
                collapsePanel.hide();
            }
        }
    }
}
