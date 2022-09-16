export { }

declare const bootstrap: any;
declare global {
    interface Window {
        initializeTooltip: any;
    }
}

window.initializeTooltip = (element, shouldBeTooltip) => {
    var tooltip = bootstrap.Tooltip.getOrCreateInstance(element);

    if (!shouldBeTooltip) {
        tooltip.dispose();
    }
};
