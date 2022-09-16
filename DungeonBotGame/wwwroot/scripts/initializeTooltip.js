window.initializeTooltip = (element, shouldBeTooltip) => {
    var tooltip = bootstrap.Tooltip.getOrCreateInstance(element);
    if (!shouldBeTooltip) {
        tooltip.dispose();
    }
};
export {};
//# sourceMappingURL=initializeTooltip.js.map