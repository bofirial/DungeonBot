export { }

declare const bootstrap: any;
declare global {
    interface Window {
        launchModal: any;
        closeModal: any;
    }
}

window.launchModal = (modalId, isClosable = true) => {
    const modalOptions = isClosable ? {} : { backdrop: 'static', keyboard: false };
    const modal = new bootstrap.Modal(document.getElementById(modalId), modalOptions);

    modal.show();
};

window.closeModal = (modalId) => {
    const modal = bootstrap.Modal.getInstance(document.getElementById(modalId));

    modal.hide();
};