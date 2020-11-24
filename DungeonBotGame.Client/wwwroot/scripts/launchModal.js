window.launchModal = (modalId, isClosable = true) => {
    const modalOptions = isClosable ? {} : { backdrop: 'static', keyboard: false };
    const modal = new bootstrap.Modal(document.getElementById(modalId), modalOptions);
    modal.show();
};
window.closeModal = (modalId) => {
    const modal = bootstrap.Modal.getInstance(document.getElementById(modalId));
    modal.hide();
};
export {};
//# sourceMappingURL=launchModal.js.map