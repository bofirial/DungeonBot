window.launchModal = (modalId, isClosable = true) => {
    var modal = bootstrap.Modal.getInstance(document.getElementById(modalId));
    if (modal == null) {
        const modalOptions = isClosable ? {} : { backdrop: 'static', keyboard: false };
        modal = new bootstrap.Modal(document.getElementById(modalId), modalOptions);
    }
    modal.show();
};
window.closeModal = (modalId) => {
    const modal = bootstrap.Modal.getInstance(document.getElementById(modalId));
    modal.hide();
};
const modalEvents = ['shown.bs.modal', 'show.bs.modal', 'hidden.bs.modal', 'hide.bs.modal'];
window.registerModalEvents = (modalId, modalComponent) => {
    window.setTimeout(() => {
        var targetModalPanel = document.getElementById(modalId);
        for (const modalEvent of modalEvents) {
            if (targetModalPanel) {
                targetModalPanel.addEventListener(modalEvent, function () {
                    modalComponent.invokeMethodAsync('TriggerEventAsync', modalEvent);
                });
            }
        }
    }, 1);
};
export {};
//# sourceMappingURL=launchModal.js.map